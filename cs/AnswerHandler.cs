﻿using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smy.cs
{
    class AnswerHandler
    {
        #region 图像处理
        public Mat imageHandle(String filePath)
        {
            Mat srcImage1 = new Mat(filePath, ImreadModes.Color);
            Mat srcImage2 = new Mat();
            Mat srcImage3 = new Mat();
            Mat srcImage4 = new Mat();
            Mat srcImage5 = new Mat();
            //Cv2.ImShow("Demo", srcImage1);
            Cv2.WaitKey(0);
            //图片变成灰度图片
            Cv2.CvtColor(srcImage1, srcImage2, ColorConversionCodes.BGR2GRAY);
            //Cv2.ImShow("Demo", srcImage2);
            //图片二值化
            Cv2.Threshold(srcImage2, srcImage3, 200, 255, ThresholdTypes.BinaryInv);
            //Cv2.ImShow("Demo", srcImage3);
            //确定腐蚀和膨胀核的大小
            Mat element = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(4, 4));
            //腐蚀操作
            Cv2.Erode(srcImage3, srcImage4, element);
            //Cv2.ImShow("Demo", srcImage4);
            //膨胀操作
            Cv2.Dilate(srcImage4, srcImage5, element);
            //Cv2.ImShow("Demo", srcImage5);
            return srcImage5;
        }
        #endregion

        #region 旋转所选区域 angle为角度
        public static void rotate(Mat src, float angle, out Mat dst)
        {
            dst = new Mat();
            Point2f center = new Point2f(src.Cols / 2, src.Rows / 2);
            Mat rot = Cv2.GetRotationMatrix2D(center, angle, 1);
            Size2f s2f = new Size2f(src.Size().Width, src.Size().Height);
            Rect box = new RotatedRect(new Point2f(0, 0), s2f, angle).BoundingRect();
            double xx = rot.At<double>(0, 2) + box.Width / 2 - src.Cols / 2;
            double zz = rot.At<double>(1, 2) + box.Height / 2 - src.Rows / 2;
            rot.Set(0, 2, xx);
            rot.Set(1, 2, zz);
            Cv2.WarpAffine(src, dst, rot, box.Size);
        }
        //rot.At<double>(0,2)不能直接用等于号赋值，要用set赋值
        #endregion
        #region 学号处理 获取学号
        public string stuNoHandle(Mat srcImage5, int xhs)
        {
            Mat imag_ch1 = new Mat(srcImage5, new Rect(307, 225, 436, 180));
            //Cv2.ImShow("Demo", imag_ch1);
            HierarchyIndex[] hierarchly1;
            Cv2.FindContours(imag_ch1, out OpenCvSharp.Point[][] chapterXh, out hierarchly1, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);
            List<Rect> rectsXh = new List<Rect>();
            for (int k = 0; k < chapterXh.Length; k++)
            {
                Rect rm = Cv2.BoundingRect(chapterXh[k]);
                //Console.WriteLine("width:" + rm.Width + ",height:" + rm.Height);
                if (rm.Width > 10 && rm.Height > 8)
                {
                    rectsXh.Add(rm);
                }
            }
            rectsXh = rectsXh.OrderBy(o => o.X).ToList();
            string[] xh = new string[xhs];
            for (int t = 0; t < rectsXh.Count; t++)
            {
                Rect r = rectsXh[t];
                int y = r.Y + r.Height / 2;
                Console.WriteLine("y：" + y);
                if (y < 24)
                {
                    xh[t] = "0";
                }
                else if (y >= 24 && y < 42)
                {
                    xh[t] = "1";
                }
                else if (42 <= y && y < 60)
                {
                    xh[t] = "2";
                }
                else if (60 <= y && y < 77)
                {
                    xh[t] = "3";
                }
                else if (77 <= y && y < 96)
                {
                    xh[t] = "4";
                }
                else if (y >= 96 && y < 113)
                {
                    xh[t] = "5";
                }
                else if (y >= 113 && y < 133)
                {
                    xh[t] = "6";
                }
                else if (y >= 133 && y < 150)
                {
                    xh[t] = "7";
                }
                else if (y >= 150 && y < 168)
                {
                    xh[t] = "8";
                }
                else if (y >= 168)
                {
                    xh[t] = "9";
                }

            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < xh.Length; i++)
            {
                sb.Append(xh[i]);
            }
            return sb.ToString();
        }
        #endregion
        #region srcImage5 处理后图片 chooseY单选区域第一行位置 chooseCount 选择题个数 chooseNum 选项个数
        public string[] simpleChooseHandle(Mat srcImage5, int chooseY, int chooseCount, int chooseNum)
        {
            int everyHangCount;
            switch (chooseNum)
            {
                case 5:
                    everyHangCount = 4;
                    break;
                case 4:
                    everyHangCount = 5;
                    break;
                default:
                    return null;
            }
            /*int[] matXFor4 = { 62, 189, 316, 443, 570 };
            int[] matXFor5 = { 62, 215, 368, 521};*/
            //获取行数
            int chooseMatCount;
            if (chooseCount % everyHangCount == 0)
            {
                chooseMatCount = chooseCount / everyHangCount;
            }
            else
            {
                chooseMatCount = chooseCount / everyHangCount + 1;
            }
            List<Mat> matList = new List<Mat>();
            string[] listenAnswer = new string[chooseCount];
            /*if (everyHangCount == 5)
            {*/
                //每行5个时即选项为4个时
                for (int k = 0; k < chooseMatCount; k++)
                {
                    int hangCount = k / everyHangCount;
                    int y = Convert.ToInt32(chooseY + hangCount * 111); //+hangCount * 18.9
                    int x = 62 + k % everyHangCount * (30 + chooseNum * 26 - 7);
                    matList.Add(new Mat(srcImage5, new Rect(x, y, 30 + chooseNum * 26 - 2, 111)));
                    //Cv2.ImShow("demo"+k, new Mat(srcImage5, new Rect(x, y, 30 + chooseNum * 26 - 2, 111)));
                }
            for (int i = 0; i < matList.Count; i++)
            {
                Mat imag_ch2 = matList[i];

                HierarchyIndex[] hierarchly2;
                Cv2.FindContours(imag_ch2, out OpenCvSharp.Point[][] chapter1, out hierarchly2, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);
                List<Rect> rects = new List<Rect>();
                for (int k = 0; k < chapter1.Length; k++)
                {
                    Rect rm = Cv2.BoundingRect(chapter1[k]);
                    //Console.WriteLine("rmx:" + rm.X + ",rmY:" + rm.Y);
                    if (rm.Width > 10 && rm.Height > 6)
                    {
                        rects.Add(rm);
                    }
                }
                if (rects.Count != 5)
                {
                    //异常处理！！！！！！！！！！！！！！！！！
                }
                rects = rects.OrderBy(o => o.Y).ToList();
                for (int t = 0; t < rects.Count; t++)
                {
                    Rect r = rects[t];
                    int x = r.X + r.Width / 2;
                    int y = r.Y + r.Height / 2;
                    switch (t)
                    {
                        case 0:
                            if (!(y < 25))
                            {
                                //异常处理!!!!!!!!!!!!!!!!!!!
                            }
                            break;
                        case 1:
                            if (!(y >= 25 && y < 44))
                            {
                                //异常处理!!!!!!!!!!!!!!!!!!!
                            }
                            break;
                        case 2:
                            if (!(y >= 44 && y < 63))
                            {
                                //异常处理!!!!!!!!!!!!!!!!!!!!!
                            }
                            break;
                        case 3:
                            if (!(y >= 63 && y < 82))
                            {
                                //异常处理!!!!!!!!!!!!!!!!!!!!
                            }
                            break;
                        case 4:
                            if (!(y >= 82))
                            {
                                //异常处理!!!!!!!!!!!!!!!!!!!!
                            }
                            break;
                    }
                    //Console.WriteLine("第"+ i +"个：" +"X:" + r.X + ",Y:" + r.Y + ",sjX::" + x + ",width:" + r.Width);
                    if (x < 30 + 6.75 * 3.78)
                    {
                        listenAnswer[t + i * 5] = "A";
                    }
                    else if (x >= 30 + 6.75 * 3.78 && x < 30 + 6.75 * 3.78 * 2)
                    {
                        listenAnswer[t + i * 5] = "B";
                    }
                    else if (x >= 30 + 6.75 * 3.78 * 2 && x < 30 + 6.75 * 3.78 * 3)
                    {
                        listenAnswer[t + i * 5] = "C";
                    }
                    else if (x >= 30 + 6.75 * 3.78 * 3 && x < 30 + 6.75 * 3.78 * 4)
                    {
                        listenAnswer[t + i * 5] = "D";
                    }
                    else if (x >= 30 + 6.75 * 3.78 * 4 && x < 30 + 6.75 * 3.78 * 5)
                    {
                        listenAnswer[t + i * 5] = "E";
                    }
                    else if (x >= 30 + 6.75 * 3.78 * 5 && x < 30 + 6.75 * 3.78 * 6)
                    {
                        listenAnswer[t + i * 5] = "F";
                    }
                    else if (x >= 30 + 6.75 * 3.78 * 6 && x < 30 + 6.75 * 3.78 * 7)
                    {
                        listenAnswer[t + i * 5] = "G";
                    }
                    else if (x >= 30 + 6.75 * 3.78 * 7)
                    {
                        listenAnswer[t + i * 5] = "H";
                    }
                }
            }
            //}
            /*else if (everyHangCount == 4)
            {
                //每行4个时即选项为5个时
                for (int k = 0; k < chooseMatCount; k++)
                {
                    int hangCount = k / everyHangCount;
                    int y = Convert.ToInt32(chooseY + hangCount * 111); //+hangCount * 18.9
                    int x = matXFor5[k % everyHangCount];
                    //Console.WriteLine("hangCount:" + hangCount + ",chooseX:" + x + ",chooseY:" + y);
                    matList.Add(new Mat(srcImage5, new Rect(x, y, 30 + chooseNum * 26 - 2, 111)));
                }
            }*/




            return listenAnswer;
        }
        #endregion
    }
}
