using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace Revit.SDK.Samples.ScheduleCreation.CS
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
        List<ViewSchedule> viewSchedules = new List<ViewSchedule>();
        /// <summary>
        /// Список основных надписей
        /// </summary>
        List<FamilySymbol> fSheets = new List<FamilySymbol>();

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
