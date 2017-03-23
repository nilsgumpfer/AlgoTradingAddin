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
    class TableObject
    {
        private Worksheet worksheet;
        private Excel.Range startPosition;
        private int startPositionRow;
        private int startPositionColumn;
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


        public void draw()
        {
            List<string> line;

            drawHeaderline();

            for (drawPosition = drawPosition; drawPosition < content.Count; drawPosition++)
            {
                line = content[drawPosition];

                for (int i = 0; i < line.Count; i++)
                {
                    worksheet.Cells[drawPosition, startPositionColumn + i] = line[i];
                }
            }
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
        


    }



}
