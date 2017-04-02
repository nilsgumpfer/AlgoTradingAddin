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
        //TODO: Methoden sortieren, schön machen
        private Workbook workbook;
        private Worksheet worksheet;
        private Excel.Range startPosition;
        private int startPositionRow;
        private int startPositionColumn;
        private List<int> columnsToDraw;
        private List<string> headline;
        private List<List<string>> content;
        private int drawPosition = 1;
        private List<DiagramObject> listOfSubscribers = new List<DiagramObject>();

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

        public void createNewWorksheet(string worksheetName)
        {
            workbook = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook);
            worksheet = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook.Worksheets.Add());
            worksheet.Name = worksheetName;
        }


        public Worksheet getWorksheetOfTableObject()
        {
            return worksheet;
        }

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

        public List<int> getColumnsToDraw()
        {
            return columnsToDraw;
        }

        public List<string> getHeadline()
        {
            return headline;
        }

        public List<List<string>> getContent()
        {
            return content;
        }

        //public void draw()
        //{
        //}


        public void drawHeaderline()
        {
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
            drawPosition++;
        }

        public void drawOnlyRelevantHeadlineColumns()
        {
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

            drawPosition++;
        }

        public void deleteOnlyRelevantHeadlineColumns()
        {
            drawPosition = startPositionRow;
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
        }

        public void deleteHeaderline()
        {
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

            drawPosition++;
        }


        public void draw()
        {
            List<string> line;

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
        }

        public void drawOnlyRelevantColumns()
        {
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
            }
        }

        public void deleteOnlyRelevantColumns()
        {
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
            }
        }

        public void deleteDraw()
        {
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
            }
        }


        public TableObject(Worksheet worksheet, Excel.Range startPosition, List<string> headline, List<List<string>> content, List<int> columnsToDraw)
        {
            this.worksheet = worksheet;
            this.startPosition = startPosition;
            startPositionRow = startPosition.Row;
            startPositionColumn = startPosition.Column;
            this.columnsToDraw = columnsToDraw;
            this.headline = headline;
            this.content = content;
        }

        public TableObject(Worksheet worksheet, Excel.Range startPosition, List<string> headline, List<List<string>> content)
        {
            this.worksheet = worksheet;
            this.startPosition = startPosition;
            startPositionRow = startPosition.Row;
            startPositionColumn = startPosition.Column;
            this.headline = headline;
            this.content = content;
        }

        public TableObject(Worksheet worksheet, Excel.Range startPosition, List<string> headline, List<string> firstLine)
        {
            this.worksheet = worksheet;
            this.startPosition = startPosition;
            startPositionRow = startPosition.Row;
            startPositionColumn = startPosition.Column;
            this.headline = headline;
            this.content = new List<List<string>>();
            content.Add(firstLine);
        }

        public TableObject(Worksheet worksheet, Excel.Range startPosition, List<StockDataTransferObject> records, List<int> columnsToDraw)
        {
            this.worksheet = worksheet;
            this.startPosition = startPosition;
            startPositionRow = startPosition.Row;
            startPositionColumn = startPosition.Column;
            this.headline = records[0].getHeadlineAsList();
            this.content = new List<List<string>>();
            this.columnsToDraw = columnsToDraw;

            foreach (StockDataTransferObject record in records)
                content.Add(record.getLineAsList());
        }

        public TableObject(Worksheet worksheet, Excel.Range startPosition)
        {
            this.worksheet = worksheet;
            this.startPosition = startPosition;
            startPositionRow = startPosition.Row;
            startPositionColumn = startPosition.Column;
            this.headline = new List<string>();
            this.content = new List<List<string>>();
        }

        public void drawAtPosition(Worksheet worksheet, Excel.Range startPosition)
        {
            this.worksheet = worksheet;
            this.startPosition = startPosition;
            startPositionRow = startPosition.Row;
            startPositionColumn = startPosition.Column;
            draw();
        }

        public void drawRelevantColumnsAtPosition(Worksheet worksheet, Excel.Range startPosition)
        {
            this.worksheet = worksheet;
            this.startPosition = startPosition;
            startPositionRow = startPosition.Row;
            startPositionColumn = startPosition.Column;
            drawOnlyRelevantColumns();
        }


        public void updateMeWithNewData(StockDataTransferObject newRecord)
        {
            headline = newRecord.getHeadlineAsList();
            content.Add(newRecord.getLineAsList());
            //TODO: nicht alles neu zeichnen, sondern nur letzte Zeile!
            draw();
            updateSubscribers();
        }

        public void changeWorkbookName(string name)
        {
            workbook = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook);
            worksheet.Name = name;
        }



    }



}
