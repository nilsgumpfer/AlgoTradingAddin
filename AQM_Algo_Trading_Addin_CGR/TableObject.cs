using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Excel;
using System.Windows.Forms;

namespace AQM_Algo_Trading_Addin_CGR
{
    class TableObject : LiveConnectionSubscriber
    {
        private Workbook workbook;
        private Worksheet worksheet;
        private Excel.Range startPosition;

        private int drawPosition;
        private int startPositionRow;
        private int startPositionColumn;

        private List<int> columnsToDraw;
        private List<string> headline;
        private List<List<string>> content;
        private List<DiagramObject> listOfSubscribers = new List<DiagramObject>();
        private string selectedDropDownTicker;
        private bool headlineVisible = false;

        //********************************************** Konstruktoren ************************************************************

        //Variante 1: Position
        public TableObject(Worksheet worksheet, Excel.Range startPosition)
        {
            this.worksheet = worksheet;
            this.startPosition = startPosition;
            initStartPosition();
            this.headline = new List<string>();
            this.content = new List<List<string>>();
            columnsToDraw = StockDataTransferObject.getStandardColumnsToDraw();
        }

        //Variante 2: Position, Headline, FirstLine
        public TableObject(Worksheet worksheet, Excel.Range startPosition, List<string> headline, List<string> firstLine)
        {
            this.worksheet = worksheet;
            this.startPosition = startPosition;
            initStartPosition();
            this.headline = headline;
            this.content = new List<List<string>>();
            content.Add(firstLine);
            columnsToDraw = StockDataTransferObject.getStandardColumnsToDraw();
        }

        //Variante 3: Position, Headline, Content
        public TableObject(Worksheet worksheet, Excel.Range startPosition, List<string> headline, List<List<string>> content)
        {
            this.worksheet = worksheet;
            this.startPosition = startPosition;
            initStartPosition();
            this.headline = headline;
            this.content = content;
            columnsToDraw = StockDataTransferObject.getStandardColumnsToDraw();
        }

        //Variante 4: Position, Headline, Content, ColumnsToDraw
        public TableObject(Worksheet worksheet, Excel.Range startPosition, List<string> headline, List<List<string>> content, List<int> columnsToDraw)
        {
            this.worksheet = worksheet;
            this.startPosition = startPosition;
            initStartPosition();
            this.columnsToDraw = columnsToDraw;
            this.headline = headline;
            this.content = content;
        }

        //Variante 5: Position, StockDataTransferObject-Liste (Content), ColumnsToDraw
        public TableObject(Worksheet worksheet, Excel.Range startPosition, List<StockDataTransferObject> records, List<int> columnsToDraw)
        {
            this.worksheet = worksheet;
            this.startPosition = startPosition;
            initStartPosition();
            if(records!=null)
                if(records.Count>0)
                    this.headline = StockDataTransferObject.getHeadlineAsList();
                else
                    this.headline = new List<string>();
            this.content = new List<List<string>>();
            this.columnsToDraw = columnsToDraw;
            if (records != null)
                foreach (StockDataTransferObject record in records)
                content.Add(record.getLineAsList());
        }

        //Variante 6: Position, ColumnsToDraw
        public TableObject(Worksheet worksheet, Excel.Range startPosition, List<int> columnsToDraw)
        {
            this.worksheet = worksheet;
            this.startPosition = startPosition;
            initStartPosition();
            this.headline = StockDataTransferObject.getHeadlineAsList();
            this.columnsToDraw = columnsToDraw;
            this.content = new List<List<string>>();
        }

        //********************************************** Konstruktoren ************************************************************

        //********************************************** Allgemeine Funktionen ****************************************************

        public int getDrawPositionOfColumn(int column)
        {
            if (columnsToDraw == null)
                return column;

            int position = 0;

            foreach (int i in columnsToDraw)
            {
                position++;
                if (i == column)
                    return position;
            }

            return -1;
        }

        public int getContentCount()
        {
            return content.Count;
        }

        public List<int> getColumnsToDraw()
        {
            return columnsToDraw;
        }

        public int getColumnsToDrawCount()
        {
            return columnsToDraw.Count;
        }

        public List<string> getHeadline()
        {
            return headline;
        }

        public List<List<string>> getContent()
        {
            return content;
        }

        public Worksheet getWorksheetOfTableObject()
        {
            return worksheet;
        }
    
        public void changeSheetName(string name)
        {
            workbook = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook);
            worksheet.Name = name;
        }

        public void createNewWorksheet(string worksheetName)
        {
            workbook = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook);
            worksheet = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook.Worksheets.Add());
            worksheet.Name = worksheetName;
        }

        public void initDrawPosition()
        {
            drawPosition = startPositionRow;
        }

        public void initStartPosition()
        {
            startPositionRow = startPosition.Row;
            startPositionColumn = startPosition.Column;
        }

        //********************************************** Allgemeine Funktionen ****************************************************

        //********************************************** Observer-Pattern *********************************************************

        public void updateSubscribers()
        {
            foreach(DiagramObject subscriber in listOfSubscribers)
            {
                subscriber.updateMeWithNewData();
            }
        }

        public void subscribeForTableContent(DiagramObject diagramobject)
        {
            listOfSubscribers.Add(diagramobject);
        }

        public void updateMeWithNewData(StockDataTransferObject newRecord)
        {
            headline = StockDataTransferObject.getHeadlineAsList();
            content.Add(newRecord.getLineAsList());

            if (headlineVisible == false)
                drawHeadline();

            drawRecord(newRecord);
            updateSubscribers();
        }

        //********************************************** Observer-Pattern *********************************************************

        //********************************************** Draw-Funktionen **********************************************************


        private void draw(bool delete)
        {
            if (delete)
                deleteHeadline();
            else
                drawHeadline();

            for (int i = 0; i < content.Count; i++)
            {
                if (delete)
                    deleteLine();
                else
                    drawLine(content[i]);
            }
        }

        public void deleteAll()
        {
            draw(true);
        }

        public void drawAll()
        {
            draw(false);
        }

        public void drawLine(List<string> line)
        {
            int space = 0;
            int columnIndex;
            string cellContent;

            if (line.Count > 0)
            {
                for (int j = 0; j < columnsToDraw.Count; j++)
                {
                    columnIndex = columnsToDraw[j];

                    try
                    {
                        if (line[columnIndex - 1] != null)
                            cellContent = line[columnIndex - 1].Replace(',', '.');
                        else
                            cellContent = "";

                        worksheet.Cells[drawPosition, startPositionColumn + space] = cellContent;
                        space++;
                    }
                    catch (Exception e)
                    {
                        j--;
                    }
                }

                drawPosition++;
            }
        }

        public void drawRecord(StockDataTransferObject record)
        {
            drawLine(record.getLineAsList());
        }

        public void deleteLine()
        {
            drawLine(null);
        }

        public void deleteHeadline()
        {
            initDrawPosition();
            drawLine(null);
            headlineVisible = false;
        }

        public void drawHeadline()
        {
            initDrawPosition();
            drawLine(headline);
            headlineVisible = true;
        }

        public void drawHeaderline()
        {/*
            drawPosition = startPositionRow;

            for(int i = 0; i < headline.Count; i++)
            {  
                try
                {
                    worksheet.Cells[drawPosition, startPositionColumn + i] = headline[i];
                }
                catch (Exception e)
                {
                    i--;
                }
            }
            drawPosition++;*/
            drawHeadline();
        }

        public void drawOnlyRelevantHeadlineColumns()
        {
            /*
            drawPosition = startPositionRow;
            int space = 0;

            foreach (int i in columnsToDraw)
            {
                try
                {
                    worksheet.Cells[drawPosition, startPositionColumn + space] = headline[i - 1];
                    space++;
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            drawPosition++;*/
            drawHeadline();
        }

        public void deleteOnlyRelevantHeadlineColumns()
        {
            /*drawPosition = startPositionRow;
            int space = 0;

            foreach (int i in columnsToDraw)
            {
                try
                {
                    worksheet.Cells[drawPosition, startPositionColumn + space] = "";
                    space++;
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            drawPosition++;*/
            deleteHeadline();
        }

        public void deleteHeaderline()
        {/*
            drawPosition = startPositionRow;

            for (int i = 0; i < headline.Count; i++)
            {
                try
                {
                    worksheet.Cells[drawPosition, startPositionColumn + i] = "";
                }
                catch (Exception e)
                {
                    i--;
                } 
            }
            drawPosition++;*/
            deleteHeadline();
        }

        public void draw()
        {
            /*
            List<string> line;

            Logger.log("draw START");

            drawHeaderline();

            for (int j = 0; j < content.Count; j++)
            {
                line = content[j];

                for (int i = 0; i < line.Count; i++)
                {
                    try
                    {
                        if(line[i] != null)
                            worksheet.Cells[drawPosition, startPositionColumn + i] = line[i].Replace(',','.');
                    }
                    catch(Exception e)
                    {
                        i--;
                    }
                }
                drawPosition++;
            }
            Logger.log("draw END");*/
            drawAll();
        }

        public void drawOnlyRelevantColumns()
        {
            /*
            List<string> line;

            drawOnlyRelevantHeadlineColumns();

            for (int j = 0; j < content.Count; j++)
            {
                line = content[j];
                int space = 0;

                foreach (int i in columnsToDraw)
                {
                    try
                    {
                        worksheet.Cells[drawPosition, startPositionColumn + space] = line[i - 1];
                        space++;
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }
                drawPosition++;
            }*/
            drawAll();
        }

        public void deleteOnlyRelevantColumns()
        {/*
            List<string> line;

            deleteOnlyRelevantHeadlineColumns();

            for (int j = 0; j < content.Count; j++)
            {
                line = content[j];
                int space = 0;

                foreach (int i in columnsToDraw)
                {
                    try
                    {
                        worksheet.Cells[drawPosition, startPositionColumn + space] = "";
                        space++;
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }
                drawPosition++;
            }*/
            deleteAll();
        }

        public void deleteDraw()
        {/*
            List<string> line;

            deleteHeaderline();

            for (int j = 0; j < content.Count; j++)
            {
                line = content[j];

                for (int i = 0; i < line.Count; i++)
                {
                    try
                    {
                        worksheet.Cells[drawPosition, startPositionColumn + i] = "";
                    }
                    catch (Exception e)
                    {
                        i--;
                    } 
                }
                drawPosition++;
            }*/
            deleteAll();
        }

        public void drawAtPosition(Worksheet worksheet, Excel.Range startPosition)
        {
            this.worksheet = worksheet;
            this.startPosition = startPosition;
            initStartPosition();
            draw();
        }

        public void drawRelevantColumnsAtPosition(Worksheet worksheet, Excel.Range startPosition)
        {
            this.worksheet = worksheet;
            this.startPosition = startPosition;
            initStartPosition();
            drawOnlyRelevantColumns();
        }
    }
}
