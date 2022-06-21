using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace ScheduleSplit.CS
{
    public class SchedulerData
    {
        /// <summary>
        /// Текущий интерфейс документа
        /// </summary>
        private UIDocument m_revitDoc;
        /// <summary>
        /// Текущий документ
        /// </summary>
        private Document doc;
        /// <summary>
        /// Список спецификаций
        /// </summary>
        public List<ViewSchedule> viewSchedules = new List<ViewSchedule>();
        /// <summary>
        /// Список основных надписей
        /// </summary>
        public List<FamilySymbol> fSheets = new List<FamilySymbol>();
        /// <summary>
        /// Список наименований основных надписей
        /// </summary>
        public List<string> fSheets_name = new List<string>();
        /// <summary>
        /// Текущая основная надпись
        /// </summary>
        public FamilySymbol fSheet = null;
        /// <summary>
        /// Текущая спецификация
        /// </summary>
        public ViewSchedule viewSchedule = null;
        /// <summary>
        /// Высота спецификции в см
        /// </summary>
        public int totalH = 0;

        public SchedulerData(ExternalCommandData commandData)
        {
            m_revitDoc = commandData.Application.ActiveUIDocument;
            doc = m_revitDoc.Document;
            GetSchedulers();
            GetSymbolSheets();
        }

        /// <summary>
        /// Получить список спецификаций
        /// </summary>
        void GetSchedulers()
        {
            IList<Element> elems = new FilteredElementCollector(doc).OfClass(typeof(ViewSchedule)).ToElements();
            
            foreach (Element e in elems)
            {
                ViewSchedule v = e as ViewSchedule;
                if (!v.IsSplit())
                {
                    viewSchedules.Add(v);                    
                }
            }
            return;
        }
        /// <summary>
        /// Получить список семейств листов
        /// </summary>
        void GetSymbolSheets()
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            IList<Element> elements = collector.OfClass(typeof(Family)).ToElements();

            foreach (Family family in elements)
            {
                if (family != null && family.GetFamilySymbolIds() != null)
                {
                    List<FamilySymbol> ffs = new List<FamilySymbol>();
                    foreach (ElementId elementId in family.GetFamilySymbolIds())
                    {
                        ffs.Add((FamilySymbol)(doc.GetElement(elementId)));
                    }
                    foreach (FamilySymbol tagSymbol in ffs)
                    {
                        try
                        {
                            if (tagSymbol != null)
                            {
                                if (tagSymbol.Category.BuiltInCategory == BuiltInCategory.OST_TitleBlocks)
                                {
                                    fSheets.Add(tagSymbol);
                                    fSheets_name.Add(tagSymbol.FamilyName + " : " + tagSymbol.Name);
                                }
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
            }
            return;
        }
    }
}
