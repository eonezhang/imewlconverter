﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Studyzy.IMEWLConverter.Entities;
using Studyzy.IMEWLConverter.Generaters;
using Studyzy.IMEWLConverter.Helpers;

namespace Studyzy.IMEWLConverter.IME
{
    /// <summary>
    /// 基于小小输入法而制作的二笔输入法，包括超强二笔、现代二笔、青松二笔等,格式：
    /// 编码 词语1 词语2 词语3
    /// </summary>
    [ComboBoxShow(ConstantString.XIAOXIAO_ERBI, ConstantString.XIAOXIAO_ERBI_C, 100)]
    public class XiaoxiaoErbi : BaseImport, IWordLibraryExport
    {
        public XiaoxiaoErbi()
        {
            form = new ErbiTypeForm();
            form.Closed += new EventHandler(form_Closed);
        }

        void form_Closed(object sender, EventArgs e)
        {
            if (form.DialogResult == DialogResult.OK)
            {
                CodeType = form.SelectedCodeType;
            }
        }

     
      
        #region IWordLibraryExport 成员
        
        public Encoding Encoding
        {
            get { return Encoding.GetEncoding("GB18030"); }
        }

        public string ExportLine(WordLibrary wl)
        {
            var sb = new StringBuilder();
            sb.Append(wl.SingleCode);
            sb.Append(" ");
            sb.Append(wl.Word);
            return sb.ToString();
        }
        private ErbiTypeForm form=new ErbiTypeForm();

        public Form ExportConfigForm { get { return form; } }

        public string Export(WordLibraryList wlList)
        {
            var sb = new StringBuilder();

            IDictionary<string, string> xiaoxiaoDic = new Dictionary<string, string>();

            for (int i = 0; i < wlList.Count; i++)
            {
                string key = "";
                var wl = wlList[i];
                string value = wl.Word;
                foreach (var code in wl.Codes)
                {
                    key = code[0];
                    if (xiaoxiaoDic.ContainsKey(key))
                    {
                        xiaoxiaoDic[key] += " " + value;
                    }
                    else
                    {
                        xiaoxiaoDic.Add(key, value);
                    }
                }
               
            }
            foreach (var keyValuePair in xiaoxiaoDic)
            {
                sb.Append(keyValuePair.Key + " " + keyValuePair.Value + "\n");
            }

            return sb.ToString();
        }

        #endregion


    
    }
}