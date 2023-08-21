using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerIAble.UI.Classes;

namespace VerIAble.UI
{
    public partial class ExportImportForm : Form
    {
        List<CustomType> customTypes;
        public ExportImportForm(List<CustomType> customTypes)
        {
            this.customTypes = customTypes;
        }



    }
}
