using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.DB.Structure;

namespace FabricSheet
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    class FabricSheet : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            // обработка перкрытий и стен при обновлении стандартных сеток
            a.ControlledApplication.DocumentOpened += new EventHandler<DocumentOpenedEventArgs>(DoEventsFabricSheet.EventUpdateFabricSheet);
            return Result.Succeeded;
        }
         
        public Result OnShutdown(UIControlledApplication a)
        {
            // a.ControlledApplication.DocumentClosed -= new EventHandler<Autodesk.Revit.DB.Events.DocumentClosedEventArgs>(DoEventsFabricSheet.EventUpdateFabricSheet);
            a.ControlledApplication.DocumentOpened -= new EventHandler<DocumentOpenedEventArgs>(DoEventsFabricSheet.EventUpdateFabricSheet);
            return Result.Succeeded;
        }
    }

    class DoEventsFabricSheet
    {

        public static void EventUpdateFabricSheet(object sender, DocumentOpenedEventArgs e)
        {
            Autodesk.Revit.ApplicationServices.Application app = sender as Autodesk.Revit.ApplicationServices.Application;
            UIApplication uiApp = new UIApplication(app);
            FabricSheetUpdater updater = new FabricSheetUpdater(uiApp.ActiveAddInId, uiApp);
            if (!UpdaterRegistry.IsUpdaterRegistered(updater.GetUpdaterId())) UpdaterRegistry.RegisterUpdater(updater);

            ElementCategoryFilter wallFilter = new ElementCategoryFilter(BuiltInCategory.OST_Walls);
            IList<ElementFilter> filterList = new List<ElementFilter>();
            filterList.Add(wallFilter);

            ElementCategoryFilter floorFilter = new ElementCategoryFilter(BuiltInCategory.OST_Floors);
            IList<ElementFilter> floorList = new List<ElementFilter>();
            filterList.Add(floorFilter);

            ElementCategoryFilter SheetFilter = new ElementCategoryFilter(BuiltInCategory.OST_FabricReinforcement);
            IList<ElementFilter> SheetList = new List<ElementFilter>();
            filterList.Add(SheetFilter);

            LogicalOrFilter filter = new LogicalOrFilter(filterList);

            UpdaterRegistry.AddTrigger(updater.GetUpdaterId(), filter, Element.GetChangeTypeAny());
            return;

        }

        /// <summary>
        /// Обновление массы сеток для стен и перекрытий
        /// </summary>

        public class FabricSheetUpdater : IUpdater
        {   
            UIApplication uiApp;
            AddInId addinID;
            UpdaterId updaterID;
            Document doc;

            public string GetAdditionalInformation() { return "Calculate weight fabric sheet"; }
            public ChangePriority GetChangePriority() { return ChangePriority.FloorsRoofsStructuralWalls; }
            public UpdaterId GetUpdaterId() { return updaterID; }
            public string GetUpdaterName() { return "Calculate weight fabric sheet"; }

            public FabricSheetUpdater(AddInId id, UIApplication muiApp)
            {

                uiApp = muiApp;
                addinID = id;
                updaterID = new UpdaterId(addinID, new Guid("54FEBF46-2AC4-4B06-9178-9129D722BF39"));
                doc = uiApp.ActiveUIDocument.Document;

            }
            public void Execute(UpdaterData data)
            {
                
                ElementCategoryFilter wallFilter = new ElementCategoryFilter(BuiltInCategory.OST_FabricReinforcement);
                IList<ElementFilter> filterList = new List<ElementFilter>();
                filterList.Add(wallFilter);

                //ElementCategoryFilter sheetFilter = new ElementCategoryFilter(BuiltInCategory.OST_FabricAreas);
                //IList<ElementFilter> sheetfilterList = new List<ElementFilter>();
                //sheetfilterList.Add(sheetFilter);


                try
                {
                    foreach (ElementId eid in data.GetModifiedElementIds())
                    {
                        double sum = 0;
                        Floor floor = doc.GetElement(eid) as Floor;
                        if (floor != null)
                        {
                            IList<ElementId> eids = floor.GetDependentElements(filterList[0]);

                            foreach (ElementId elementId in eids)
                            {
                                // ElementId typeId = doc.GetElement(elementId).GetTypeId();
                                // получить текущий тип
                                Element el = doc.GetElement(elementId);
                                sum = sum + GetFABRIC_SHEET_MASS(el);
                            }

                            //eids.Clear();
                            //eids = floor.GetDependentElements(sheetfilterList[0]);

                            //foreach (ElementId elementId in eids)
                            //{
                            //    FabricArea fa = doc.GetElement(elementId) as FabricArea;
                            //    if(fa != null) sum = sum + GetAREA_REINF(fa);
                            //}

                            SetParameter(floor, sum);
                            break;
                        }

                        sum = 0;
                        Wall wall = doc.GetElement(eid) as Wall;
                        if (wall != null)
                        {
                            IList<ElementId> eids = wall.GetDependentElements(filterList[0]);

                            foreach (ElementId elementId in eids)
                            {
                                // ElementId typeId = doc.GetElement(elementId).GetTypeId();
                                // получить текущий тип
                                Element el = doc.GetElement(elementId);
                                sum = sum + GetFABRIC_SHEET_MASS(el);
                            }

                            //eids.Clear();
                            //eids = wall.GetDependentElements(sheetfilterList[0]);

                            //foreach (ElementId elementId in eids)
                            //{
                            //    FabricArea fa = doc.GetElement(elementId) as FabricArea;
                            //    if (fa != null) sum = sum + GetAREA_REINF(fa);
                            //}

                            SetParameter(wall, sum);
                            break;
                        }

                        //sum = 0;
                        //FabricSheetType sheettype = doc.GetElement(eid) as FabricSheetType;

                        //if (sheettype != null)
                        //{
                        //    IEnumerable<Element> elems = new FilteredElementCollector(doc).OfClass(typeof(Autodesk.Revit.DB.Structure.FabricSheet)).
                        //        Where(x => x.GetTypeId().IntegerValue == sheettype.Id.IntegerValue);

                        //    foreach (Element e in elems)
                        //    {
                        //        Autodesk.Revit.DB.Structure.FabricSheet v = e as Autodesk.Revit.DB.Structure.FabricSheet;
                        //        ElementId eidHost = v.HostId;
                        //        sum = 0;
                        //        Element element = doc.GetElement(eidHost);
                        //        if (element != null)
                        //        {
                        //            IList<ElementId> eids = element.GetDependentElements(filterList[0]);

                        //            foreach (ElementId elementId in eids)
                        //            {
                        //                //ElementId typeId = doc.GetElement(elementId).GetTypeId();
                        //                // получить текущий тип
                        //                Element el = doc.GetElement(elementId);
                        //                sum = sum + GetFABRIC_SHEET_MASS(el);
                        //            }

                        //            SetParameter(element, sum);
                        //            break;
                        //        }

                        //    }

                        //    break;
                        //}


                        Autodesk.Revit.DB.Structure.FabricSheet fabricarea = doc.GetElement(eid) as Autodesk.Revit.DB.Structure.FabricSheet;
                        if (fabricarea != null)
                        {        
                                ElementId eidHost = fabricarea.HostId;
                                sum = 0;
                                Element element = doc.GetElement(eidHost);
                                if (element != null)
                                {
                                    IList<ElementId> eids = element.GetDependentElements(filterList[0]);

                                    foreach (ElementId elementId in eids)
                                    {    
                                        Element el = doc.GetElement(elementId);
                                        sum = sum + GetFABRIC_SHEET_MASS(el);
                                    }

                                    SetParameter(element, sum);
                                    break;
                                }                            
                        }

                    }
                }
                catch { }                

            }


            /// <summary>
            /// Назначить параметр
            /// </summary>
            bool SetParameter(Element fi, double value)
            {
                ParameterSet paras = fi.Parameters;
                Parameter findPara = FindParaByName(paras, "Fabric sheet (weight)");

                if (null == findPara)
                {
                    return false;
                }

                if (!findPara.IsReadOnly)
                {
                    findPara.Set(value);
                    return true;
                }

                return false; 
            }

            /// <summary>
            /// find certain parameter in a set
            /// </summary>
            /// <param name="paras"></param>
            /// <param name="name">find by name</param>
            /// <returns>found parameter</returns>
            public static Parameter FindParaByName(ParameterSet paras, string name)
            {
                Parameter findPara = null;

                foreach (Parameter para in paras)
                {
                    if (para.Definition.Name == name)
                    {
                        findPara = para;
                    }
                }

                return findPara;
            }

            /// <summary>
            /// Получить значение параметра
            /// </summary>
            double GetFABRIC_SHEET_MASS(Element fi)
            {
                Parameter parameter = fi.get_Parameter(BuiltInParameter.FABRIC_PARAM_CUT_SHEET_MASS);
                if (parameter == null) return 0;                 
                return parameter.AsDouble();
            }
            /// <summary>
            /// Получить значение параметра
            /// </summary>
            double GetAREA_REINF(Element fi)
            {
                Parameter parameter = fi.get_Parameter(BuiltInParameter.FABRIC_PARAM_TOTAL_SHEET_MASS);
                if (parameter == null) return 0;
                return parameter.AsDouble();
            }
        }
    }


}
