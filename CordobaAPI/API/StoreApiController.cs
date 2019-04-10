using CordobaModels.Entities;
using CordobaServices.Helpers;
using CordobaServices.Interfaces;
using CordobaServices.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Spire.Presentation;
using Spire.Presentation.Charts;
using Spire.Presentation.Drawing;
//using Spire.Presentation;
//using Spire.Presentation.Charts;
//using Spire.Presentation.Drawing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace CordobaAPI.API
{
    public class StoreApiController : ApiController
    {
        public IStoreServices _StoreServices;

        public StoreApiController()
        {
            _StoreServices = new StoreServices();
        }


        [HttpGet]
        public HttpResponseMessage GetStoreList(int? StoreID, int LoggedInUserId)
        {
            try
            {
                var result = _StoreServices.GetStoreList(StoreID, LoggedInUserId);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        public HttpResponseMessage GetStoreById(int store_id, int LoggedInUserId)
        {
            try
            {
                var result = _StoreServices.GetStoreById(store_id, LoggedInUserId);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public HttpResponseMessage InsertUpdateStore(int LoggedInUserId, StoreEntity storeEntity)
        {
            try
            {
                var result = _StoreServices.InsertUpdateStore(storeEntity, LoggedInUserId);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage DeleteStoreById_Admin(int store_id, int LoggedInUserId)
        {
            try
            {
                var result = _StoreServices.DeleteStoreById_Admin(store_id, LoggedInUserId);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong? Please try again later.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/StoreApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/StoreApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/StoreApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/StoreApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/StoreApi/5
        public void Delete(int id)
        {
        }

        [HttpPost]
        public HttpResponseMessage UploadStoreImage(int Store_Id, int ImageKey, int layout, string Store_Name)
        {
            bool res = false;
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files[0];

                if (httpPostedFile != null)
                {
                    string folderPath = ConfigurationManager.AppSettings["FileUploadPath"].ToString() + "data//" + CordobaCommon.Enum.CommonEnums.FolderName.StoreImage.ToString();
                    if (!string.IsNullOrWhiteSpace(folderPath))
                    {
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        string childFolderPath = folderPath + "/" + Store_Id;
                        if (!Directory.Exists(childFolderPath))
                        {
                            Directory.CreateDirectory(childFolderPath);
                        }

                        childFolderPath += "/" + ImageKey;
                        if (!Directory.Exists(childFolderPath))
                        {
                            Directory.CreateDirectory(childFolderPath);
                        }

                        string fileName = Store_Id + "/" + ImageKey + "/" + Store_Name + "_Image.png"; //before httpPostedFile.FileName
                        res = _StoreServices.UploadStoreImage(Store_Id, "data/" + CordobaCommon.Enum.CommonEnums.FolderName.StoreImage.ToString() + "/" + fileName, ImageKey, layout);

                        if (res == true)
                        {
                            httpPostedFile.SaveAs(folderPath + "\\" + fileName);

                            var directoryFiles = Directory.GetFiles(childFolderPath);
                            foreach (var filepath in directoryFiles)
                            {
                                if (Path.GetFileName(filepath) != Store_Name + "_Image.png") //before httpPostedFile.FileName
                                {
                                    File.Delete(filepath);
                                }
                            }
                        }
                    }
                }
            }

            if (res == true)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { data = true });
            }

            return Request.CreateResponse(HttpStatusCode.NotImplemented, new { data = false });
        }

        [HttpPost]
        public HttpResponseMessage UploadStoreLogo(int store_id, string store_name)
        {
            bool res = false;
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files[0];

                if (httpPostedFile != null)
                {
                    string folderPath = ConfigurationManager.AppSettings["FileUploadPath"].ToString() + "data//" + CordobaCommon.Enum.CommonEnums.FolderName.store_logos.ToString();
                    if (!string.IsNullOrWhiteSpace(folderPath))
                    {
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }



                        string fileName = store_name + "-" + httpPostedFile.FileName;
                        res = _StoreServices.UploadStoreLogo(store_id, "data/" + CordobaCommon.Enum.CommonEnums.FolderName.store_logos.ToString() + "/" + fileName);

                        if (res == true)
                        {
                            httpPostedFile.SaveAs(folderPath + "\\" + fileName);

                            var directoryFiles = Directory.GetFiles(folderPath);
                            foreach (var filepath in directoryFiles)
                            {
                                if (Path.GetFileName(filepath) != httpPostedFile.FileName)
                                {
                                    //File.Delete(filepath);
                                }
                            }
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.OK);


            }
            if (res == true)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { data = true });
            }

            return Request.CreateResponse(HttpStatusCode.NotImplemented, new { data = false });
        }

        [HttpGet]
        public HttpResponseMessage GetAdvertisementImageList(int store_id)
        {
            try
            {
                var result = _StoreServices.GetAdvertisementImageList(store_id);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong? Please try again later.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public HttpResponseMessage GetStoreHTMLCharts(int StoreID, int Month, int Year)
        {
            //Spire Presentation DLL version 3.3.0
            try
            {
                var result = _StoreServices.GetStoreHTMLCharts(StoreID, Month, Year);

                Presentation ppt = new Presentation();
                ISlide slide = ppt.Slides[0];

                //First slide
                SizeF pptSize = ppt.SlideSize.Size;

                IAutoShape shape = (IAutoShape)ppt.Slides[0].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(0, 0, 720, 100));
                shape.Fill.FillType = FillFormatType.Solid;
                shape.Fill.SolidColor.Color = Color.Red;
                shape.ShapeStyle.LineColor.Color = Color.Brown;

                //ppt.Slides[0].Shapes.ZOrder(1, shape);

                ////insert image to PPT
                //string ImageFile2 = HttpContext.Current.Server.MapPath("~/Content//images//logo//") + "red.png";
                //RectangleF fisrtsliderect = new RectangleF(10, 10, 600, 100);
                //ppt.Slides[0].Shapes.AppendEmbedImage(ShapeType.Rectangle, ImageFile2, fisrtsliderect);
                //ppt.Slides[0].Shapes[0].Line.FillFormat.SolidFillColor.Color = Color.FloralWhite;



                string logoFile = HttpContext.Current.Server.MapPath("~/Content//images//logo//") + "cordoba-logo.png";//"logo.png";
                //RectangleF logoRect = new RectangleF(pptSize.Width / 2 - 40, 60, 80, 80);
                RectangleF logoRect = new RectangleF(10, 180, 200, 78);
                IEmbedImage image = slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, logoFile, logoRect);
                image.Line.FillType = FillFormatType.None;

                string logoFile1 = HttpContext.Current.Server.MapPath("~/Content//images//Storelogo//") + StoreID + ".png";//"logo.png";
                //RectangleF logoRect = new RectangleF(pptSize.Width / 2 - 40, 60, 80, 80);
                RectangleF logoRect1 = new RectangleF(500, 180, 200, 78);
                IEmbedImage image1 = slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, logoFile1, logoRect1);
                image1.Line.FillType = FillFormatType.None;

                ////Add content
                //RectangleF textRect = new RectangleF(10, 200, 200, 130);
                //IAutoShape textShape = slide.Shapes.AppendShape(ShapeType.Rectangle, textRect);
                ////Content format
                //string text = "Review Meeting";
                //textShape.AppendTextFrame(text);
                RectangleF titleRect = new RectangleF(10, 270, 200, 60);
                IAutoShape titleShape = slide.Shapes.AppendShape(ShapeType.Rectangle, titleRect);
                titleShape.Fill.FillType = FillFormatType.None;
                titleShape.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph titlePara = titleShape.TextFrame.Paragraphs[0];
                titlePara.Text = "Review Meeting";
                titlePara.FirstTextRange.FontHeight = 20;
                titlePara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                titlePara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                titlePara.Alignment = TextAlignmentType.Center;

                RectangleF titleRect1 = new RectangleF(10, 290, 200, 60);
                IAutoShape titleShape1 = slide.Shapes.AppendShape(ShapeType.Rectangle, titleRect1);
                titleShape1.Fill.FillType = FillFormatType.None;
                titleShape1.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph titlePara1 = titleShape1.TextFrame.Paragraphs[0];
                string monthvalue = string.Empty;
                switch (Month)
                {
                    case 1:
                        monthvalue = "January";
                        break;
                    case 2:
                        monthvalue = "February";
                        break;
                    case 3:
                        monthvalue = "March";
                        break;
                    case 4:
                        monthvalue = "April";
                        break;
                    case 5:
                        monthvalue = "May";
                        break;
                    case 6:
                        monthvalue = "June";
                        break;
                    case 7:
                        monthvalue = "July";
                        break;
                    case 8:
                        monthvalue = "August";
                        break;
                    case 9:
                        monthvalue = "September";
                        break;
                    case 10:
                        monthvalue = "October";
                        break;
                    case 11:
                        monthvalue = "November";
                        break;
                    default:
                        monthvalue = "December";
                        break;

                }
                titlePara1.Text = string.Format("{0} {1}", monthvalue, "2018");
                titlePara1.FirstTextRange.FontHeight = 20;
                titlePara1.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                titlePara1.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                titlePara1.Alignment = TextAlignmentType.Center;

                string logoFile2 = HttpContext.Current.Server.MapPath("~/Content//images//logo//") + "homepdfimage.png";//"logo.png";
                //RectangleF logoRect = new RectangleF(pptSize.Width / 2 - 40, 60, 80, 80);
                RectangleF logoRect2 = new RectangleF(10, 350, 700, 160);
                IEmbedImage image2 = slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, logoFile2, logoRect2);
                image2.Line.FillType = FillFormatType.None;

                //Set footer
                ppt.SetDateTimeVisible(false);
                ppt.SetSlideNoVisible(true);

                ////Set transition of slide
                //slide.SlideShowTransition.Type = TransitionType.Cover;
                //ISlide slide1 = ppt.Slides[0];
                //ppt.Slides.Append(slide);
                ppt.Slides.Append();

                //Second slide
                //RectangleF storerect = new RectangleF(10, 100, 550, 320);
                //IChart storechart = ppt.Slides[1].Shapes.AppendChart(ChartType.ColumnClustered, storerect);

                //IAutoShape storeshape = (IAutoShape)ppt.Slides[1].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(10, 10, 700, 50));
                //storeshape.Fill.FillType = FillFormatType.Solid;
                //storeshape.Fill.SolidColor.Color = Color.Red;
                //storeshape.ShapeStyle.LineColor.Color = Color.White;

                RectangleF titleRectSlide2 = new RectangleF(10, 10, 200, 60);
                IAutoShape titleShapeSlide2 = ppt.Slides[1].Shapes.AppendShape(ShapeType.Rectangle, titleRectSlide2);
                titleShapeSlide2.Fill.FillType = FillFormatType.None;
                titleShapeSlide2.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph titleParaSlide2 = titleShapeSlide2.TextFrame.Paragraphs[0];
                titleParaSlide2.Text = "Store";
                titleParaSlide2.FirstTextRange.FontHeight = 30;
                titleParaSlide2.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                titleParaSlide2.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                titleParaSlide2.Alignment = TextAlignmentType.Center;

                string logoFileslide2 = HttpContext.Current.Server.MapPath("~/Content//images//Storelogo//") + StoreID + ".png";//"logo.png";
                //RectangleF logoRect = new RectangleF(pptSize.Width / 2 - 40, 60, 80, 80);
                RectangleF logoRectslide2 = new RectangleF(500, 10, 200, 78);
                IEmbedImage imageslide2 = ppt.Slides[1].Shapes.AppendEmbedImage(ShapeType.Rectangle, logoFileslide2, logoRectslide2);
                imageslide2.Line.FillType = FillFormatType.None;


                string storeFile = HttpContext.Current.Server.MapPath("~/Content//images//Storescreen//") + StoreID + ".png";//"logo.png";
                //RectangleF logoRect = new RectangleF(pptSize.Width / 2 - 40, 60, 80, 80);
                RectangleF storeRect = new RectangleF(110, 100, 600, 380);
                IEmbedImage storeimage = ppt.Slides[1].Shapes.AppendEmbedImage(ShapeType.Rectangle, storeFile, storeRect);
                storeimage.Line.FillType = FillFormatType.None;

                //Third slide
                ppt.Slides.Append();

                string logoFileslide3 = HttpContext.Current.Server.MapPath("~/Content//images//Storelogo//") + StoreID + ".png";//"logo.png";
                //RectangleF logoRect = new RectangleF(pptSize.Width / 2 - 40, 60, 80, 80);
                RectangleF logoRectslide3 = new RectangleF(500, 10, 200, 78);
                IEmbedImage imageslide3 = ppt.Slides[2].Shapes.AppendEmbedImage(ShapeType.Rectangle, logoFileslide3, logoRectslide3);
                imageslide3.Line.FillType = FillFormatType.None;

                RectangleF storesummarytitleRect = new RectangleF(10, 100, 400, 30);
                IAutoShape storesummarytitleShape = ppt.Slides[2].Shapes.AppendShape(ShapeType.Rectangle, storesummarytitleRect);
                storesummarytitleShape.Fill.FillType = FillFormatType.None;
                storesummarytitleShape.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph storesummarytitlePara = storesummarytitleShape.TextFrame.Paragraphs[0];
                storesummarytitlePara.Text = "Store Summary";
                storesummarytitlePara.FirstTextRange.FontHeight = 30;
                storesummarytitlePara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                storesummarytitlePara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                storesummarytitlePara.Alignment = TextAlignmentType.Center;

                RectangleF dougnutrect = new RectangleF(10, 200, 320, 300);
                IChart chart = ppt.Slides[2].Shapes.AppendChart(ChartType.Doughnut, dougnutrect, false);
                chart.ChartTitle.TextProperties.Text = "PARTICIPANTS (" + Convert.ToInt32(result.storeSummary.ToList()[0].Count + result.storeSummary.ToList()[1].Count) + ")";//"Store Summary";
                chart.ChartTitle.TextProperties.IsCentered = true;
                chart.ChartTitle.Height = 30;



                //IAutoShape storesummaryshape = (IAutoShape)ppt.Slides[2].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(10, 10, 700, 50));
                //storesummaryshape.Fill.FillType = FillFormatType.Solid;
                //storesummaryshape.Fill.SolidColor.Color = Color.Red;
                //storesummaryshape.ShapeStyle.LineColor.Color = Color.White;

                string[] countries = new string[] { "Active", "Not Active" };
                int[] sales = new int[] { result.storeSummary[0].Count, result.storeSummary[1].Count };
                chart.ChartData[0, 0].Text = "Countries";
                chart.ChartData[0, 1].Text = "Sales";
                for (int i = 0; i < countries.Length; ++i)
                {
                    chart.ChartData[i + 1, 0].Value = countries[i];
                    chart.ChartData[i + 1, 1].Value = sales[i];
                }
                chart.Series.SeriesLabel = chart.ChartData["B1", "B1"];
                chart.Categories.CategoryLabels = chart.ChartData["A2", "A3"];
                chart.Series[0].Values = chart.ChartData["B2", "B3"];
                //for (int i = 0; i < chart.Series[0].Values.Count; i++)
                for (int i = 0; i < 2; i++)
                {
                    ChartDataPoint cdp = new ChartDataPoint(chart.Series[0]);
                    cdp.Index = i;
                    chart.Series[0].DataPoints.Add(cdp);
                }
                chart.Series[0].DataPoints[0].Fill.FillType = FillFormatType.Solid;
                chart.Series[0].DataPoints[0].Fill.SolidColor.Color = Color.LightBlue;
                chart.Series[0].DataPoints[1].Fill.FillType = FillFormatType.Solid;
                chart.Series[0].DataPoints[1].Fill.SolidColor.Color = Color.MediumPurple;
                chart.Series[0].DataLabels.LabelValueVisible = true;
                //chart.Series[0].DataLabels.PercentValueVisible = true;
                //chart.Series[0].DoughnutHoleSize = 60;

                //Point Summary

                RectangleF pointsummaryrect = new RectangleF(400, 200, 320, 300);
                IChart pointsummarychart = ppt.Slides[2].Shapes.AppendChart(ChartType.Doughnut, pointsummaryrect, false);
                pointsummarychart.ChartTitle.TextProperties.Text = "POINTS (" +Convert.ToInt32(result.pointsRemaining[0].Count + result.pointsRemaining[1].Count) + ")";
                pointsummarychart.ChartTitle.TextProperties.IsCentered = true;
                pointsummarychart.ChartTitle.Height = 30;
                string[] pointsummarycountries = new string[] { "Activated accounts", "Non activated accounts" };
                int[] pointsummarysales = new int[] { result.pointsRemaining[0].Count, result.pointsRemaining[1].Count };
                pointsummarychart.ChartData[0, 0].Text = "Countries";
                pointsummarychart.ChartData[0, 1].Text = "Sales";
                for (int i = 0; i < pointsummarycountries.Length; ++i)
                {
                    pointsummarychart.ChartData[i + 1, 0].Value = pointsummarycountries[i];
                    pointsummarychart.ChartData[i + 1, 1].Value = pointsummarysales[i];
                }

                pointsummarychart.Series.SeriesLabel = pointsummarychart.ChartData["B1", "B1"];
                pointsummarychart.Categories.CategoryLabels = pointsummarychart.ChartData["A2", "A3"];
                pointsummarychart.Series[0].Values = pointsummarychart.ChartData["B2", "B3"];
                //for (int i = 0; i < chart.Series[0].Values.Count; i++)
                for (int i = 0; i < 2; i++)
                {
                    ChartDataPoint cdp = new ChartDataPoint(pointsummarychart.Series[0]);
                    cdp.Index = i;
                    pointsummarychart.Series[0].DataPoints.Add(cdp);
                }
                pointsummarychart.Series[0].DataPoints[0].Fill.FillType = FillFormatType.Solid;
                pointsummarychart.Series[0].DataPoints[0].Fill.SolidColor.Color = Color.LightBlue;
                pointsummarychart.Series[0].DataPoints[1].Fill.FillType = FillFormatType.Solid;
                pointsummarychart.Series[0].DataPoints[1].Fill.SolidColor.Color = Color.MediumPurple;
                pointsummarychart.Series[0].DataLabels.LabelValueVisible = true;

                // pointsummarychart.Series[0].DoughnutHoleSize = 60;

                //Fourth slide
                ppt.Slides.Append();


                string logoFileslide4 = HttpContext.Current.Server.MapPath("~/Content//images//Storelogo//") + StoreID + ".png";//"logo.png";
                //RectangleF logoRect = new RectangleF(pptSize.Width / 2 - 40, 60, 80, 80);
                RectangleF logoRectslide4 = new RectangleF(500, 10, 200, 78);
                IEmbedImage imageslide4 = ppt.Slides[3].Shapes.AppendEmbedImage(ShapeType.Rectangle, logoFileslide4, logoRectslide4);
                imageslide4.Line.FillType = FillFormatType.None;

                //insert a column chart  
                //insert a column participantloadedbymonthchart  
                RectangleF rect = new RectangleF(10, 100, 800, 400);
                IChart participantloadedbymonthchart = ppt.Slides[3].Shapes.AppendChart(ChartType.ColumnClustered, rect);

                //IAutoShape participantloadedshape = (IAutoShape)ppt.Slides[3].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(10, 10, 700, 50));
                //participantloadedshape.Fill.FillType = FillFormatType.Solid;
                //participantloadedshape.Fill.SolidColor.Color = Color.Red;
                //participantloadedshape.ShapeStyle.LineColor.Color = Color.White;

                //set chart title  
                participantloadedbymonthchart.ChartTitle.TextProperties.Text = "Participants loaded by month";
                participantloadedbymonthchart.ChartTitle.TextProperties.IsCentered = true;
                participantloadedbymonthchart.ChartTitle.Height = 30;
                participantloadedbymonthchart.HasTitle = true;

                //create a datatable  
                System.Data.DataTable dataTable1 = new DataTable();
                System.Data.DataTable dataTablenew = ToDataTable(result.participantsLoadedByMonth.ToList());
                //dataTable1 = dataTablenew.Copy();
                dataTable1.Columns.Add(new DataColumn("Month", Type.GetType("System.String")));
                dataTable1.Columns.Add(new DataColumn("CustomerCount", Type.GetType("System.Int32")));
                dataTable1.Columns.Add(new DataColumn("Chart", Type.GetType("System.Decimal")));

                //dataTable1.Rows.Clear();
                for (int i = 0; i < dataTablenew.Rows.Count; i++)
                {
                    //dataTable1.Rows.Add("Customer" + i.ToString(), 0);
                    dataTable1.ImportRow(dataTablenew.Rows[i]);
                }

                //import data from datatable to chart data  
                for (int c = 0; c < dataTable1.Columns.Count; c++)
                {
                    participantloadedbymonthchart.ChartData[0, c].Text = dataTable1.Columns[c].Caption;
                }
                for (int r = 0; r < dataTable1.Rows.Count; r++)
                {
                    object[] datas = dataTable1.Rows[r].ItemArray;
                    for (int c = 0; c < datas.Length; c++)
                    {
                        participantloadedbymonthchart.ChartData[r + 1, c].Value = datas[c];

                    }
                }

                //set series labels  
                participantloadedbymonthchart.Series.SeriesLabel = participantloadedbymonthchart.ChartData["B1", "C1"];

                int totalRows = dataTable1.Rows.Count + 1;

                //set categories labels      
                participantloadedbymonthchart.Categories.CategoryLabels = participantloadedbymonthchart.ChartData["A2", "A" + totalRows.ToString()];

                //assign data to series values  
                participantloadedbymonthchart.Series[0].Values = participantloadedbymonthchart.ChartData["B2", "B" + totalRows.ToString()];
                participantloadedbymonthchart.Series[1].Values = participantloadedbymonthchart.ChartData["C2", "C" + totalRows.ToString()];

                //change the chart type of series 2 to line chart with markers  
                participantloadedbymonthchart.Series[1].Type = ChartType.LineMarkers;

                ////plot data of series 2 on the secondary axis  
                //participantloadedbymonthchart.Series[1].UseSecondAxis = true;

                ////set the number format as percentage   
                //participantloadedbymonthchart.SecondaryValueAxis.NumberFormat = "0%";

                //hide grid lines of secondary axis  
                participantloadedbymonthchart.SecondaryValueAxis.MajorGridTextLines.FillType = FillFormatType.None;

                //set overlap  
                //participantloadedbymonthchart.OverLap = -50;

                //set gap width  
                participantloadedbymonthchart.GapWidth = 200;

                
                //Sixth slide
                ppt.Slides.Append();

                string logoFileslide6 = HttpContext.Current.Server.MapPath("~/Content//images//Storelogo//") + StoreID + ".png";//"logo.png";
                //RectangleF logoRect = new RectangleF(pptSize.Width / 2 - 40, 60, 80, 80);
                RectangleF logoRectslide6 = new RectangleF(500, 10, 200, 78);
                IEmbedImage imageslide6 = ppt.Slides[4].Shapes.AppendEmbedImage(ShapeType.Rectangle, logoFileslide6, logoRectslide6);
                imageslide6.Line.FillType = FillFormatType.None;

                //TopPointsHoldersByStore[] pointhoders = result.topPointsHolders.ToList().ToArray();
                DataTable dtpointhoders = ToDataTable(result.topPointsHolders.ToList());

                //IAutoShape pointhodersshape = (IAutoShape)ppt.Slides[4].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(10, 10, 700, 50));
                //pointhodersshape.Fill.FillType = FillFormatType.Solid;
                //pointhodersshape.Fill.SolidColor.Color = Color.Red;
                //pointhodersshape.ShapeStyle.LineColor.Color = Color.White;
                RectangleF pointhoderstitleRect = new RectangleF(10, 30, 200, 30);
                IAutoShape pointhoderstitleShape = ppt.Slides[4].Shapes.AppendShape(ShapeType.Rectangle, pointhoderstitleRect);
                pointhoderstitleShape.Fill.FillType = FillFormatType.None;
                pointhoderstitleShape.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph pointhoderstitlePara = pointhoderstitleShape.TextFrame.Paragraphs[0];
                pointhoderstitlePara.Text = "Top Points Holders";
                pointhoderstitlePara.FirstTextRange.FontHeight = 20;
                pointhoderstitlePara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                pointhoderstitlePara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                pointhoderstitlePara.Alignment = TextAlignmentType.Center;

                Double[] widths = new double[] { 100, 100, 100, 100, 100 };
                Double[] heights = new double[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
                ITable table = ppt.Slides[4].Shapes.AppendTable(ppt.SlideSize.Size.Width / 2 - 275, 150, widths, heights);
                //set the style of table
                table.StylePreset = TableStylePreset.LightStyle1Accent2;


                //    String[,] dataStr = new String[,]{
                //{"Name",    "Capital",  "Continent",    "Area", "Population"},
                //{"Venezuela",   "Caracas",  "South America",    "912047",   "19700000"},
                //{"Bolivia", "La Paz",   "South America",    "1098575",  "7300000"},
                //{"Brazil",  "Brasilia", "South America",    "8511196",  "150400000"},
                //{"Canada",  "Ottawa",   "North America",    "9976147",  "26500000"},
                //{"Chile",   "Santiago", "South America",    "756943",   "13200000"},
                //{"Colombia",    "Bagota",   "South America",    "1138907",  "33000000"},
                //{"Cuba",    "Havana",   "North America",    "114524",   "10600000"},
                //{"Ecuador", "Quito",    "South America",    "455502",   "10600000"},
                //{"Paraguay",    "Asuncion","South America", "406576",   "4660000"},
                //{"Peru",    "Lima", "South America",    "1285215",  "21600000"},
                //{"Jamaica", "Kingston", "North America",    "11424",    "2500000"},
                //{"Mexico",  "Mexico City",  "North America",    "1967180",  "88600000"}
                //};

                //for (int i = 0; i < 13; i++)
                //    for (int j = 0; j < 5; j++)
                //    {
                //        //fill the table with data
                //        table[j, i].TextFrame.Text = dataStr[i, j];
                //        //set the Font
                //        table[j, i].TextFrame.Paragraphs[0].TextRanges[0].LatinFont = new TextFont("Arial Narrow");
                //    }

                for (int i = 0; i < dtpointhoders.Columns.Count; i++)
                {
                    table[i, 0].TextFrame.Text = dtpointhoders.Columns[i].ColumnName.ToString();// dataStr[i, j];
                }

                for (int i = 0; i < dtpointhoders.Rows.Count; i++)
                    for (int j = 0; j < dtpointhoders.Columns.Count; j++)
                    {
                        //fill the table with data
                        if (j == 4)
                        {
                            table[j, i + 1].TextFrame.Text = dtpointhoders.Rows[i][j].ToString().Split(' ')[0];
                        }
                        else
                        {
                            table[j, i + 1].TextFrame.Text = dtpointhoders.Rows[i][j].ToString();
                        }
                        //set the Font
                        table[j, i + 1].TextFrame.Paragraphs[0].TextRanges[0].LatinFont = new TextFont("Arial Narrow");
                        table[j, i + 1].TextFrame.Paragraphs[0].TextRanges[0].FontHeight = 10;
                    }

                //set the alignment of the first row to Center
                for (int i = 0; i < 6; i++)
                {
                    table[i, 0].TextFrame.Paragraphs[0].Alignment = TextAlignmentType.Center;
                }

                //Seventh slide
                //Points Loaded By Month
                ppt.Slides.Append();
                string logoFileslide7 = HttpContext.Current.Server.MapPath("~/Content//images//Storelogo//") + StoreID + ".png";//"logo.png";
                //RectangleF logoRect = new RectangleF(pptSize.Width / 2 - 40, 60, 80, 80);
                RectangleF logoRectslide7 = new RectangleF(500, 10, 200, 78);
                IEmbedImage imageslide7 = ppt.Slides[5].Shapes.AppendEmbedImage(ShapeType.Rectangle, logoFileslide7, logoRectslide7);
                imageslide7.Line.FillType = FillFormatType.None;

                //insert a column chart  
                //insert a column participantloadedbymonthchart  
                RectangleF pointsloadedrect = new RectangleF(10, 120, 700, 400);
                IChart pointsloadedbymonthchart = ppt.Slides[5].Shapes.AppendChart(ChartType.ColumnClustered, pointsloadedrect);

                //IAutoShape participantloadedshape = (IAutoShape)ppt.Slides[3].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(10, 10, 700, 50));
                //participantloadedshape.Fill.FillType = FillFormatType.Solid;
                //participantloadedshape.Fill.SolidColor.Color = Color.Red;
                //participantloadedshape.ShapeStyle.LineColor.Color = Color.White;

                //set chart title  
                pointsloadedbymonthchart.ChartTitle.TextProperties.Text = "Points loaded by month";
                pointsloadedbymonthchart.ChartTitle.TextProperties.IsCentered = true;
                pointsloadedbymonthchart.ChartTitle.Height = 30;
                pointsloadedbymonthchart.HasTitle = true;

                //create a datatable  
                System.Data.DataTable pointsloadeddataTable1 = new DataTable();
                System.Data.DataTable pointsloadeddataTablenew = ToDataTable(result.pointsLoadedByMonth.ToList());
                //dataTable1 = dataTablenew.Copy();
                pointsloadeddataTable1.Columns.Add(new DataColumn("Month", Type.GetType("System.String")));
                pointsloadeddataTable1.Columns.Add(new DataColumn("Points", Type.GetType("System.Int32")));
                pointsloadeddataTable1.Columns.Add(new DataColumn("Chart", Type.GetType("System.Decimal")));

                //dataTable1.Rows.Clear();
                for (int i = 0; i < pointsloadeddataTablenew.Rows.Count; i++)
                {
                    //dataTable1.Rows.Add("Customer" + i.ToString(), 0);
                    pointsloadeddataTable1.ImportRow(pointsloadeddataTablenew.Rows[i]);
                }

                //import data from datatable to chart data  
                for (int c = 0; c < pointsloadeddataTable1.Columns.Count; c++)
                {
                    pointsloadedbymonthchart.ChartData[0, c].Text = pointsloadeddataTable1.Columns[c].Caption;
                }
                for (int r = 0; r < pointsloadeddataTable1.Rows.Count; r++)
                {
                    object[] datas = pointsloadeddataTable1.Rows[r].ItemArray;
                    for (int c = 0; c < datas.Length; c++)
                    {
                        pointsloadedbymonthchart.ChartData[r + 1, c].Value = datas[c];

                    }
                }

                //set series labels  
                pointsloadedbymonthchart.Series.SeriesLabel = pointsloadedbymonthchart.ChartData["B1", "C1"];

                int pointsloadedtotalRows = pointsloadeddataTable1.Rows.Count + 1;

                //set categories labels      
                pointsloadedbymonthchart.Categories.CategoryLabels = pointsloadedbymonthchart.ChartData["A2", "A" + pointsloadedtotalRows.ToString()];

                //assign data to series values  
                pointsloadedbymonthchart.Series[0].Values = pointsloadedbymonthchart.ChartData["B2", "B" + pointsloadedtotalRows.ToString()];
                pointsloadedbymonthchart.Series[1].Values = pointsloadedbymonthchart.ChartData["C2", "C" + pointsloadedtotalRows.ToString()];

                //change the chart type of series 2 to line chart with markers  
                pointsloadedbymonthchart.Series[1].Type = ChartType.LineMarkers;

                ////plot data of series 2 on the secondary axis  
                //participantloadedbymonthchart.Series[1].UseSecondAxis = true;

                ////set the number format as percentage   
                //participantloadedbymonthchart.SecondaryValueAxis.NumberFormat = "0%";

                //hide grid lines of secondary axis  
                pointsloadedbymonthchart.SecondaryValueAxis.MajorGridTextLines.FillType = FillFormatType.None;

                //set overlap  
                //participantloadedbymonthchart.OverLap = -50;

                //set gap width  
                pointsloadedbymonthchart.GapWidth = 200;




                //Eightth slide
                //Points Redeemed By Month
                ppt.Slides.Append();

                string logoFileslide8 = HttpContext.Current.Server.MapPath("~/Content//images//Storelogo//") + StoreID + ".png";//"logo.png";
                //RectangleF logoRect = new RectangleF(pptSize.Width / 2 - 40, 60, 80, 80);
                RectangleF logoRectslide8 = new RectangleF(500, 10, 200, 78);
                IEmbedImage imageslide8 = ppt.Slides[6].Shapes.AppendEmbedImage(ShapeType.Rectangle, logoFileslide8, logoRectslide8);
                imageslide8.Line.FillType = FillFormatType.None;

                //insert a column chart  
                //insert a column participantloadedbymonthchart  
                RectangleF pointsredeemedtitleRect = new RectangleF(10, 120, 700, 400);
                IChart pointsredeemedbymonthchart = ppt.Slides[6].Shapes.AppendChart(ChartType.ColumnClustered, pointsredeemedtitleRect);

                //IAutoShape participantloadedshape = (IAutoShape)ppt.Slides[3].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(10, 10, 700, 50));
                //participantloadedshape.Fill.FillType = FillFormatType.Solid;
                //participantloadedshape.Fill.SolidColor.Color = Color.Red;
                //participantloadedshape.ShapeStyle.LineColor.Color = Color.White;

                //set chart title  
                pointsredeemedbymonthchart.ChartTitle.TextProperties.Text = "Points Reedemed By Month";
                pointsredeemedbymonthchart.ChartTitle.TextProperties.IsCentered = true;
                pointsredeemedbymonthchart.ChartTitle.Height = 30;
                pointsredeemedbymonthchart.HasTitle = true;

                //create a datatable  
                System.Data.DataTable pointsredeemdataTable1 = new DataTable();
                //System.Data.DataTable pointsredeemdataTablenew = ToDataTable(result.participantsLoadedByMonth.ToList());
                System.Data.DataTable pointsredeemdataTablenew = ToDataTable(result.pointsRedeemedByMonth.ToList());
                //dataTable1 = dataTablenew.Copy();
                pointsredeemdataTable1.Columns.Add(new DataColumn("Month", Type.GetType("System.String")));
                pointsredeemdataTable1.Columns.Add(new DataColumn("Points", Type.GetType("System.Int32")));
                pointsredeemdataTable1.Columns.Add(new DataColumn("Chart", Type.GetType("System.Decimal")));

                //dataTable1.Rows.Clear();
                for (int i = 0; i < pointsredeemdataTablenew.Rows.Count; i++)
                {
                    //dataTable1.Rows.Add("Customer" + i.ToString(), 0);
                    pointsredeemdataTable1.ImportRow(pointsredeemdataTablenew.Rows[i]);
                }

                //import data from datatable to chart data  
                for (int c = 0; c < pointsredeemdataTable1.Columns.Count; c++)
                {
                    pointsredeemedbymonthchart.ChartData[0, c].Text = pointsredeemdataTable1.Columns[c].Caption;
                }
                for (int r = 0; r < pointsredeemdataTable1.Rows.Count; r++)
                {
                    object[] datas = pointsredeemdataTable1.Rows[r].ItemArray;
                    for (int c = 0; c < datas.Length; c++)
                    {
                        pointsredeemedbymonthchart.ChartData[r + 1, c].Value = datas[c];

                    }
                }

                //set series labels  
                pointsredeemedbymonthchart.Series.SeriesLabel = pointsredeemedbymonthchart.ChartData["B1", "C1"];

                int pointsredeemedtotalRows = pointsredeemdataTable1.Rows.Count + 1;

                //set categories labels      
                pointsredeemedbymonthchart.Categories.CategoryLabels = pointsredeemedbymonthchart.ChartData["A2", "A" + pointsredeemedtotalRows.ToString()];

                //assign data to series values  
                pointsredeemedbymonthchart.Series[0].Values = pointsredeemedbymonthchart.ChartData["B2", "B" + pointsredeemedtotalRows.ToString()];
                pointsredeemedbymonthchart.Series[1].Values = pointsredeemedbymonthchart.ChartData["C2", "C" + pointsredeemedtotalRows.ToString()];

                //change the chart type of series 2 to line chart with markers  
                pointsredeemedbymonthchart.Series[1].Type = ChartType.LineMarkers;

                ////plot data of series 2 on the secondary axis  
                //participantloadedbymonthchart.Series[1].UseSecondAxis = true;

                ////set the number format as percentage   
                //participantloadedbymonthchart.SecondaryValueAxis.NumberFormat = "0%";

                //hide grid lines of secondary axis  
                pointsredeemedbymonthchart.SecondaryValueAxis.MajorGridTextLines.FillType = FillFormatType.None;

                //set overlap  
                //participantloadedbymonthchart.OverLap = -50;

                //set gap width  
                pointsredeemedbymonthchart.GapWidth = 200;

                //Fifth slide
                ppt.Slides.Append();

                string logoFileslide5 = HttpContext.Current.Server.MapPath("~/Content//images//Storelogo//") + StoreID + ".png";//"logo.png";
                //RectangleF logoRect = new RectangleF(pptSize.Width / 2 - 40, 60, 80, 80);
                RectangleF logoRectslide5 = new RectangleF(500, 10, 200, 78);
                IEmbedImage imageslide5 = ppt.Slides[7].Shapes.AppendEmbedImage(ShapeType.Rectangle, logoFileslide5, logoRectslide5);
                imageslide5.Line.FillType = FillFormatType.None;

                RectangleF OrderPlacedByTypeByMonthrect = new RectangleF(10, 100, 700, 400);
                //IAutoShape orderplacedbytypemonthshape = (IAutoShape)ppt.Slides[7].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(10, 10, 700, 50));
                //orderplacedbytypemonthshape.Fill.FillType = FillFormatType.Solid;
                //orderplacedbytypemonthshape.Fill.SolidColor.Color = Color.Red;
                //orderplacedbytypemonthshape.ShapeStyle.LineColor.Color = Color.White;




                //create a datatable  
                System.Data.DataTable dtMakeOrderPlacedByTypeByMonth = new DataTable();
                System.Data.DataTable dataTableMakeOrderPlacedByTypeDynamic = ToDataTable(result.orderPlacedByType.ToList());

                if (dataTableMakeOrderPlacedByTypeDynamic != null && dataTableMakeOrderPlacedByTypeDynamic.Rows.Count > 0)
                {
                    IChart OrderPlacedByTypeByMonthchart = ppt.Slides[7].Shapes.AppendChart(ChartType.ColumnClustered, OrderPlacedByTypeByMonthrect);


                    //set chart title  
                    OrderPlacedByTypeByMonthchart.ChartTitle.TextProperties.Text = "Order Placed By Type";
                    OrderPlacedByTypeByMonthchart.ChartTitle.TextProperties.IsCentered = true;
                    OrderPlacedByTypeByMonthchart.ChartTitle.Height = 30;
                    OrderPlacedByTypeByMonthchart.HasTitle = true;

                    dtMakeOrderPlacedByTypeByMonth.Columns.Add(new DataColumn("Name", Type.GetType("System.String")));
                    dtMakeOrderPlacedByTypeByMonth.Columns.Add(new DataColumn("OrderCount", Type.GetType("System.Int32")));
                    dtMakeOrderPlacedByTypeByMonth.Columns.Add(new DataColumn("Chart", Type.GetType("System.Decimal")));

                    //dataTable1.Rows.Clear();
                    for (int i = 0; i < dataTableMakeOrderPlacedByTypeDynamic.Rows.Count; i++)
                    {
                        //dataTable1.Rows.Add("Customer" + i.ToString(), 0);
                        dtMakeOrderPlacedByTypeByMonth.ImportRow(dataTableMakeOrderPlacedByTypeDynamic.Rows[i]);
                    }

                    //import data from datatable to chart data  
                    for (int c = 0; c < dtMakeOrderPlacedByTypeByMonth.Columns.Count; c++)
                    {
                        OrderPlacedByTypeByMonthchart.ChartData[0, c].Text = dtMakeOrderPlacedByTypeByMonth.Columns[c].Caption;
                    }
                    for (int r = 0; r < dtMakeOrderPlacedByTypeByMonth.Rows.Count; r++)
                    {
                        object[] datas = dtMakeOrderPlacedByTypeByMonth.Rows[r].ItemArray;
                        for (int c = 0; c < datas.Length; c++)
                        {
                            OrderPlacedByTypeByMonthchart.ChartData[r + 1, c].Value = datas[c];

                        }
                    }

                    //set series labels  
                    OrderPlacedByTypeByMonthchart.Series.SeriesLabel = OrderPlacedByTypeByMonthchart.ChartData["B1", "C1"];

                    int totalRowsOrderPlacedByTypeByMonthchart = dtMakeOrderPlacedByTypeByMonth.Rows.Count + 1;

                    //set categories labels      
                    OrderPlacedByTypeByMonthchart.Categories.CategoryLabels = OrderPlacedByTypeByMonthchart.ChartData["A2", "A" + totalRowsOrderPlacedByTypeByMonthchart.ToString()];

                    //assign data to series values  
                    OrderPlacedByTypeByMonthchart.Series[0].Values = OrderPlacedByTypeByMonthchart.ChartData["B2", "B" + totalRowsOrderPlacedByTypeByMonthchart.ToString()];
                    OrderPlacedByTypeByMonthchart.Series[1].Values = OrderPlacedByTypeByMonthchart.ChartData["C2", "C" + totalRowsOrderPlacedByTypeByMonthchart.ToString()];

                    //change the chart type of series 2 to line chart with markers  
                    OrderPlacedByTypeByMonthchart.Series[1].Type = ChartType.LineMarkers;

                    ////plot data of series 2 on the secondary axis  
                    //OrderPlacedByTypeByMonthchart.Series[1].UseSecondAxis = true;

                    ////set the number format as percentage   
                    //OrderPlacedByTypeByMonthchart.SecondaryValueAxis.NumberFormat = "0%";

                    //hide grid lines of secondary axis  
                    OrderPlacedByTypeByMonthchart.SecondaryValueAxis.MajorGridTextLines.FillType = FillFormatType.None;

                    //set overlap  
                    //OrderPlacedByTypeByMonthchart.OverLap = -50;

                    //set gap width  
                    OrderPlacedByTypeByMonthchart.GapWidth = 200;
                }
                else
                {
                    RectangleF OrderPlacedByTypeByMonthtitleRect1 = new RectangleF(60, 10, 500, 130);
                    IAutoShape OrderPlacedByTypeByMonthtitleShape1 = ppt.Slides[7].Shapes.AppendShape(ShapeType.Rectangle, OrderPlacedByTypeByMonthtitleRect1);
                    OrderPlacedByTypeByMonthtitleShape1.Fill.FillType = FillFormatType.None;
                    OrderPlacedByTypeByMonthtitleShape1.ShapeStyle.LineColor.Color = Color.Empty;
                    TextParagraph OrderPlacedByTypeByMonthtitlePara1 = OrderPlacedByTypeByMonthtitleShape1.TextFrame.Paragraphs[0];
                    OrderPlacedByTypeByMonthtitlePara1.Text = "Order Placed By Type";
                    OrderPlacedByTypeByMonthtitlePara1.FirstTextRange.FontHeight = 20;
                    OrderPlacedByTypeByMonthtitlePara1.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                    OrderPlacedByTypeByMonthtitlePara1.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                    OrderPlacedByTypeByMonthtitlePara1.Alignment = TextAlignmentType.Center;

                    RectangleF OrderPlacedByTypeByMonthtitleRect = new RectangleF(60, 100, 500, 130);
                    IAutoShape OrderPlacedByTypeByMonthtitleShape = ppt.Slides[7].Shapes.AppendShape(ShapeType.Rectangle, OrderPlacedByTypeByMonthtitleRect);
                    OrderPlacedByTypeByMonthtitleShape.Fill.FillType = FillFormatType.None;
                    OrderPlacedByTypeByMonthtitleShape.ShapeStyle.LineColor.Color = Color.Empty;
                    TextParagraph OrderPlacedByTypeByMonthtitlePara = OrderPlacedByTypeByMonthtitleShape.TextFrame.Paragraphs[0];
                    OrderPlacedByTypeByMonthtitlePara.Text = "No Orders Available.";
                    OrderPlacedByTypeByMonthtitlePara.FirstTextRange.FontHeight = 20;
                    OrderPlacedByTypeByMonthtitlePara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                    OrderPlacedByTypeByMonthtitlePara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                    OrderPlacedByTypeByMonthtitlePara.Alignment = TextAlignmentType.Center;
                }

                ////Ninth slide
                //ppt.Slides.Append();
                //string logoFileslide9 = HttpContext.Current.Server.MapPath("~/Content//images//Storelogo//") + StoreID + ".png";//"logo.png";
                ////RectangleF logoRect = new RectangleF(pptSize.Width / 2 - 40, 60, 80, 80);
                //RectangleF logoRectslide9 = new RectangleF(500, 10, 200, 78);
                //IEmbedImage imageslide9 = ppt.Slides[8].Shapes.AppendEmbedImage(ShapeType.Rectangle, logoFileslide9, logoRectslide9);
                //imageslide9.Line.FillType = FillFormatType.None;

                ////insert a column chart  
                ////insert a column participantloadedbymonthchart  
                //RectangleF vouchercountrect9 = new RectangleF(10, 100, 700, 400);
                //IChart vouchercountchart9 = ppt.Slides[8].Shapes.AppendChart(ChartType.ColumnClustered, vouchercountrect9);

                ////IAutoShape participantloadedshape = (IAutoShape)ppt.Slides[3].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(10, 10, 700, 50));
                ////participantloadedshape.Fill.FillType = FillFormatType.Solid;
                ////participantloadedshape.Fill.SolidColor.Color = Color.Red;
                ////participantloadedshape.ShapeStyle.LineColor.Color = Color.White;

                ////set chart title  
                //vouchercountchart9.ChartTitle.TextProperties.Text = "Voucher Orders By Type";
                //vouchercountchart9.ChartTitle.TextProperties.IsCentered = true;
                //vouchercountchart9.ChartTitle.Height = 30;
                //vouchercountchart9.HasTitle = true;

                ////create a datatable  
                //System.Data.DataTable vouchercountdataTable19 = new DataTable();
                //System.Data.DataTable vouchercountdataTablenew9 = ToDataTable(result.voucherOrderByType.ToList());
                ////dataTable1 = dataTablenew.Copy();
                //vouchercountdataTable19.Columns.Add(new DataColumn("ReportCategoryName", Type.GetType("System.String")));
                //vouchercountdataTable19.Columns.Add(new DataColumn("Orders", Type.GetType("System.Int32")));
                //vouchercountdataTable19.Columns.Add(new DataColumn("Chart", Type.GetType("System.Decimal")));

                ////dataTable1.Rows.Clear();
                //for (int i = 0; i < vouchercountdataTablenew9.Rows.Count; i++)
                //{
                //    //dataTable1.Rows.Add("Customer" + i.ToString(), 0);
                //    vouchercountdataTable19.ImportRow(vouchercountdataTablenew9.Rows[i]);
                //}

                ////import data from datatable to chart data  
                //for (int c = 0; c < vouchercountdataTable19.Columns.Count; c++)
                //{
                //    vouchercountchart9.ChartData[0, c].Text = vouchercountdataTable19.Columns[c].Caption;
                //}
                //for (int r = 0; r < vouchercountdataTable19.Rows.Count; r++)
                //{
                //    object[] datas = vouchercountdataTable19.Rows[r].ItemArray;
                //    for (int c = 0; c < datas.Length; c++)
                //    {
                //        vouchercountchart9.ChartData[r + 1, c].Value = datas[c];

                //    }
                //}

                ////set series labels  
                //vouchercountchart9.Series.SeriesLabel = vouchercountchart9.ChartData["B1", "C1"];

                //int vouchercounttotalRows9 = vouchercountdataTable19.Rows.Count + 1;

                ////set categories labels      
                //vouchercountchart9.Categories.CategoryLabels = vouchercountchart9.ChartData["A2", "A" + vouchercounttotalRows9.ToString()];

                ////assign data to series values  
                //vouchercountchart9.Series[0].Values = vouchercountchart9.ChartData["B2", "B" + vouchercounttotalRows9.ToString()];
                //vouchercountchart9.Series[1].Values = vouchercountchart9.ChartData["C2", "C" + vouchercounttotalRows9.ToString()];

                ////change the chart type of series 2 to line chart with markers  
                //vouchercountchart9.Series[1].Type = ChartType.LineMarkers;

                //////plot data of series 2 on the secondary axis  
                ////participantloadedbymonthchart.Series[1].UseSecondAxis = true;

                //////set the number format as percentage   
                ////participantloadedbymonthchart.SecondaryValueAxis.NumberFormat = "0%";

                ////hide grid lines of secondary axis  
                //vouchercountchart9.SecondaryValueAxis.MajorGridTextLines.FillType = FillFormatType.None;

                ////set overlap  
                ////participantloadedbymonthchart.OverLap = -50;

                ////set gap width  
                //vouchercountchart9.GapWidth = 200;

                ////Tenth
                //ppt.Slides.Append();
                //string logoFileslide10 = HttpContext.Current.Server.MapPath("~/Content//images//Storelogo//") + StoreID + ".png";//"logo.png";
                ////RectangleF logoRect = new RectangleF(pptSize.Width / 2 - 40, 60, 80, 80);
                //RectangleF logoRectslide10 = new RectangleF(500, 10, 200, 78);
                //IEmbedImage imageslide10 = ppt.Slides[9].Shapes.AppendEmbedImage(ShapeType.Rectangle, logoFileslide10, logoRectslide10);
                //imageslide10.Line.FillType = FillFormatType.None;

                ////insert a column chart  
                ////insert a column participantloadedbymonthchart  
                //RectangleF ordersPlacedByTypeRect = new RectangleF(10, 100, 700, 400);
                //IChart ordersPlacedByTypechart = ppt.Slides[9].Shapes.AppendChart(ChartType.ColumnClustered, ordersPlacedByTypeRect);

                ////IAutoShape participantloadedshape = (IAutoShape)ppt.Slides[3].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(10, 10, 700, 50));
                ////participantloadedshape.Fill.FillType = FillFormatType.Solid;
                ////participantloadedshape.Fill.SolidColor.Color = Color.Red;
                ////participantloadedshape.ShapeStyle.LineColor.Color = Color.White;

                ////set chart title  
                //ordersPlacedByTypechart.ChartTitle.TextProperties.Text = "Orders placed by Type";
                //ordersPlacedByTypechart.ChartTitle.TextProperties.IsCentered = true;
                //ordersPlacedByTypechart.ChartTitle.Height = 30;
                //ordersPlacedByTypechart.HasTitle = true;

                ////create a datatable  
                //System.Data.DataTable ordersPlacedByTypechartaTable1 = new DataTable();
                //System.Data.DataTable ordersPlacedByTypechartTablenew = ToDataTable(result.ordersPlacedByTypeLastYear.ToList());
                ////dataTable1 = dataTablenew.Copy();
                //ordersPlacedByTypechartaTable1.Columns.Add(new DataColumn("name", Type.GetType("System.String")));
                //ordersPlacedByTypechartaTable1.Columns.Add(new DataColumn("Orders", Type.GetType("System.Int32")));
                //ordersPlacedByTypechartaTable1.Columns.Add(new DataColumn("Chart", Type.GetType("System.Decimal")));

                ////dataTable1.Rows.Clear();
                //for (int i = 0; i < ordersPlacedByTypechartTablenew.Rows.Count; i++)
                //{
                //    //dataTable1.Rows.Add("Customer" + i.ToString(), 0);
                //    ordersPlacedByTypechartaTable1.ImportRow(ordersPlacedByTypechartTablenew.Rows[i]);
                //}

                ////import data from datatable to chart data  
                //for (int c = 0; c < ordersPlacedByTypechartaTable1.Columns.Count; c++)
                //{
                //    ordersPlacedByTypechart.ChartData[0, c].Text = ordersPlacedByTypechartaTable1.Columns[c].Caption;
                //}
                //for (int r = 0; r < ordersPlacedByTypechartaTable1.Rows.Count; r++)
                //{
                //    object[] datas = ordersPlacedByTypechartaTable1.Rows[r].ItemArray;
                //    for (int c = 0; c < datas.Length; c++)
                //    {
                //        ordersPlacedByTypechart.ChartData[r + 1, c].Value = datas[c];

                //    }
                //}

                ////set series labels  
                //ordersPlacedByTypechart.Series.SeriesLabel = ordersPlacedByTypechart.ChartData["B1", "C1"];

                //int ordersPlacedByTypetotalRows = ordersPlacedByTypechartaTable1.Rows.Count + 1;

                ////set categories labels      
                //ordersPlacedByTypechart.Categories.CategoryLabels = ordersPlacedByTypechart.ChartData["A2", "A" + ordersPlacedByTypetotalRows.ToString()];

                ////assign data to series values  
                //ordersPlacedByTypechart.Series[0].Values = ordersPlacedByTypechart.ChartData["B2", "B" + ordersPlacedByTypetotalRows.ToString()];
                //ordersPlacedByTypechart.Series[1].Values = ordersPlacedByTypechart.ChartData["C2", "C" + ordersPlacedByTypetotalRows.ToString()];

                ////change the chart type of series 2 to line chart with markers  
                //ordersPlacedByTypechart.Series[1].Type = ChartType.LineMarkers;

                //////plot data of series 2 on the secondary axis  
                ////participantloadedbymonthchart.Series[1].UseSecondAxis = true;

                //////set the number format as percentage   
                ////participantloadedbymonthchart.SecondaryValueAxis.NumberFormat = "0%";

                ////hide grid lines of secondary axis  
                //ordersPlacedByTypechart.SecondaryValueAxis.MajorGridTextLines.FillType = FillFormatType.None;

                ////set overlap  
                ////participantloadedbymonthchart.OverLap = -50;

                ////set gap width  
                //ordersPlacedByTypechart.GapWidth = 200;

                //save to file  
                ppt.SaveToFile(HttpContext.Current.Server.MapPath("~/Files//") + "Monthly Report.pptx", FileFormat.Pptx2010);
                //presentation.SaveToFile(HttpContext.Current.Server.MapPath("~/Files//") + "CombinationChart.pptx", FileFormat.Pptx2010);

                HttpResponseMessage response = Request.CreateResponse();

                string filePath = HttpContext.Current.Server.MapPath("~/Files//") + "Monthly Report.pptx";
                FileInfo fileInfo = new FileInfo(filePath);

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = GetMimeType("Monthly Report.pptx");
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Monthly Report.pptx");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.WriteFile(HttpContext.Current.Server.MapPath("~/Files//") + "Monthly Report.pptx");

                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong? Please try again later.");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static Stream ReadStream(FileInfo fileInfo)
        {
            int bufferSize = 1048575; // 1MB
            return new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize);
        }

        private static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        [HttpPost]
        public HttpResponseMessage ExportStoreHTMLPDF(StoreEntity storeentity)
        {


            // var htmlContent1 = storeentity.template;//String.Format("<body>Hello world: {0}</body>", ZTime.Now);
            // var htmlToPdf1 = new NReco.PdfGenerator.HtmlToPdfConverter();
            // var pdfBytes1 = htmlToPdf1.GeneratePdf(htmlContent1);
            // //var headingfile = File.Create(HttpContext.Current.Server.MapPath("~/Files//") + "Heading.pdf");
            // //headingfile.Write(pdfBytes1,0,pdfBytes1.Length);
            // //File.Open(HttpContext.Current.Server.MapPath("~/Files//") + "Heading.pdf", FileMode.Open);
            // File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Files//") + "Heading.pdf", pdfBytes1);

            // var htmlContent2 = storeentity.description;
            // var htmlToPdf2 = new NReco.PdfGenerator.HtmlToPdfConverter();
            // var pdfBytes2 = htmlToPdf2.GeneratePdf(htmlContent2);
            // File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Files//") + "StoreImage.pdf", pdfBytes2);

            // var htmlContent3 = storeentity.address;
            // var htmlToPdf3 = new NReco.PdfGenerator.HtmlToPdfConverter();
            // var pdfBytes3 = htmlToPdf3.GeneratePdf(htmlContent3);
            // File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Files//") + "StoreSummary.pdf", pdfBytes3);

            // var htmlContent4 = storeentity.county;
            // var htmlToPdf4 = new NReco.PdfGenerator.HtmlToPdfConverter();
            // var pdfBytes4 = htmlToPdf4.GeneratePdf(htmlContent4);
            // File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Files//") + "PointsRemaining.pdf", pdfBytes4);

            // var htmlContent5 = storeentity.telephone;
            // var htmlToPdf5 = new NReco.PdfGenerator.HtmlToPdfConverter();
            // var pdfBytes5 = htmlToPdf5.GeneratePdf(htmlContent5);
            // File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Files//") + "ParticipantsLoadedByMonth.pdf", pdfBytes5);


            // MergePDFs(HttpContext.Current.Server.MapPath("~/Files//") + "Heading.pdf",
            //     HttpContext.Current.Server.MapPath("~/Files//") + "StoreImage.pdf", 
            //     HttpContext.Current.Server.MapPath("~/Files//") + "StoreSummary.pdf",
            //     HttpContext.Current.Server.MapPath("~/Files//") + "PointsRemaining.pdf",
            //     HttpContext.Current.Server.MapPath("~/Files//") + "ParticipantsLoadedByMonth.pdf");

            //// headingfile.Close();

            HttpResponseMessage httpResponseMessage;
            httpResponseMessage = new HttpResponseMessage();
            ////HttpResponseMessage streamContent = new HttpResponseMessage(HttpStatusCode.OK);
            ////Stream @null = Stream.Null;

            ////streamContent.Content = new StreamContent(new MemoryStream(pdfBytes1));
            ////streamContent.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            ////streamContent.Content.Headers.Add("content-disposition", string.Concat("inline;  filename=\"", "Create.pdf", "\""));
            ////httpResponseMessage = streamContent;

            ////return httpResponseMessage;
            return httpResponseMessage;
        }

        private void MergePDFs(params string[] filesPath)
        {
            List<PdfReader> readerList = new List<PdfReader>();
            foreach (string filePath in filesPath)
            {
                PdfReader pdfReader = new PdfReader(filePath);
                readerList.Add(pdfReader);
            }

            //Define a new output document and its size, type
            Document document = new Document(PageSize.A4, 0, 0, 0, 0);
            //Create blank output pdf file and get the stream to write on it.
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(HttpContext.Current.Server.MapPath("~/Files//") + "Merge.pdf", FileMode.Create));
            document.Open();

            foreach (PdfReader reader in readerList)
            {
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    PdfImportedPage page = writer.GetImportedPage(reader, i);
                    document.Add(iTextSharp.text.Image.GetInstance(page));
                }
            }
            document.Close();

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Store PDF.pdf");
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.WriteFile(HttpContext.Current.Server.MapPath("~/Files//") + "Merge.pdf");

            System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            //if (File.Exists(HttpContext.Current.Server.MapPath("~/Files//Heading.pdf")))
            //{

            //    File.Delete(HttpContext.Current.Server.MapPath("~/Files//Heading.pdf"));
            //}
            //if (File.Exists(HttpContext.Current.Server.MapPath("~/Files//StoreSummary.pdf")))
            //{
            //    File.Delete(HttpContext.Current.Server.MapPath("~/Files//StoreSummary.pdf"));
            //}
            //if (File.Exists(HttpContext.Current.Server.MapPath("~/Files//Merge.pdf")))
            //{
            //    File.Delete(HttpContext.Current.Server.MapPath("~/Files//Merge.pdf"));
            //}
        }


        
    }
}
