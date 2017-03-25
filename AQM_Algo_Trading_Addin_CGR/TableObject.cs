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
        private Worksheet worksheet;
        private Excel.Range startPosition;
        private int startPositionRow;
        private int startPositionColumn;
        private List<int> columnsToDraw;
        private List<string> headline;
        private List<List<string>> content;
        private int drawPosition = 1;

        public void drawHeaderline()
        {
            drawPosition = startPositionRow;

            for(int i = 0; i < headline.Count; i++)
            {
                worksheet.Cells[drawPosition, startPositionColumn + i] = headline[i];
            }

            drawPosition++;
        }

        public void drawOnlyRelevantHeadlineColumns()
        {
            drawPosition = startPositionRow;

            foreach (int i in columnsToDraw)
            {
                int space = 0;
                worksheet.Cells[drawPosition, startPositionColumn + space] = headline[i];
                space++;
            }

            drawPosition++;
        }

        public void deleteOnlyRelevantHeadlineColumns()
        {
            drawPosition = startPositionRow;

            foreach (int i in columnsToDraw)
            {
                int space = 0;
                worksheet.Cells[drawPosition, startPositionColumn + space] = "";
                space++;
            }

            drawPosition++;
        }

        public void deleteHeaderline()
        {
            drawPosition = startPositionRow;

            for (int i = 0; i < headline.Count; i++)
            {
                worksheet.Cells[drawPosition, startPositionColumn + i] = "";
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
                    worksheet.Cells[drawPosition, startPositionColumn + i] = line[i];
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

                foreach (int i in columnsToDraw)
                {
                    int space = 0;

                    worksheet.Cells[drawPosition, startPositionColumn + space] = line[i];

                    space++;
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

                foreach (int i in columnsToDraw)
                {
                    int space = 0;

                    worksheet.Cells[drawPosition, startPositionColumn + space] = "";

                    space++;
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
                    worksheet.Cells[drawPosition, startPositionColumn + i] = "";
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
        }
    }



}
