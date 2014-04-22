using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternOrientedRefactoringSimplification
{
    public class XMLBuilder
    {
        private string name;

        public XMLBuilder(string p)
        {
            // TODO: Complete member initialization
            this.name = p;
        }

        internal void addBelowParent(string p)
        {
            throw new NotImplementedException();
        }

        internal void addAttribute(string p1, object p2)
        {
            throw new NotImplementedException();
        }
    }
    public class WorkshopRepository
    {
        Dictionary<string, Workshop> workshop = new Dictionary<string,Workshop>();
        public Dictionary<string, Workshop> Workshop { get { return workshop; } }
    
        public Workshop getWorkshop(string id)
        {
 	        return workshop[id];
        }
    }

    public class Workshop { public object ID { get; set; }
    public object Name { get; set; }

    public object Status { get; set; }

    internal object getDurationAsString()
    {
        throw new NotImplementedException();
    }
    }
    public class WorkshopManager
    {
        public string NextWorkshopID { set; get; }
        public string WorkshopDir { get; set; }
        public string WorkshopTemplate { get; set; }

        internal StringBuilder createNewFileFromTemplate(string nextWorkshopID, string workshopDir, string workshopTemplate)
        {
            StringBuilder return_string = new StringBuilder();
            return_string.Append(nextWorkshopID);
            return_string.Append(workshopTemplate);
            return_string.Append(workshopTemplate);
            return return_string;
        }

        internal void addWorkshop(StringBuilder newWorkshopContents)
        {
            throw new NotImplementedException();
        }

        internal WorkshopRepository getWorkshopRepository()
        {
            throw new NotImplementedException();
        }
    }

    public class HandlerResponse
    {
        private StringBuilder stringBuilder;
        private string ALL_WORKSHOPS_STYLESHEET;

        public HandlerResponse(StringBuilder stringBuilder, string ALL_WORKSHOPS_STYLESHEET)
        {
            // TODO: Complete member initialization
            this.stringBuilder = stringBuilder;
            this.ALL_WORKSHOPS_STYLESHEET = ALL_WORKSHOPS_STYLESHEET;
        }
    }

    public class CatalogApp
    {
        public readonly string NEW_WORKSHOP = "NEW_WORKSHOP";
        public readonly string ALL_WORKSHOPS = "ALL_WORKSHOPS";

        private Dictionary<string, Handler> handlers;

        WorkshopManager workshopManager;
        public WorkshopManager WorkshopManager { get { return workshopManager; } }

        CatalogApp()
        {
            workshopManager = new WorkshopManager();
            createHandlers();
        }

        private void createHandlers()
        {
            handlers = new Dictionary<string, Handler>();
            handlers.Add(NEW_WORKSHOP, new NewWorkshopResponse(this));
            handlers.Add(ALL_WORKSHOPS, new AllWorkshopsResponse(this));
        }

        public HandlerResponse executeActionAndGetResponse(String actionName, Dictionary<string, string> parameters)
        {
            if (actionName.Equals(NEW_WORKSHOP))
            {
                return new NewWorkshopResponse(this).execute(parameters);
            }
            else if (actionName.Equals(ALL_WORKSHOPS))
            {
                return new AllWorkshopsResponse(this).execute(parameters);
            }
            return null;
        }
    }

    #region WorkshopsResponse
    public abstract class Handler
    {
        protected CatalogApp catalogApp;

        public Handler(CatalogApp catalogApp)
        {
            this.catalogApp = catalogApp;
        }

        public abstract HandlerResponse execute(Dictionary<string, string> parameters);
    }

    public class AllWorkshopsResponse:Handler
    {
        private readonly string ALL_WORKSHOPS_STYLESHEET = "ALL_WORKSHOPS_STYLESHEET";
        public AllWorkshopsResponse(CatalogApp catalogApp):base(catalogApp){}

        public override HandlerResponse execute(Dictionary<string, string> parameters)
        {
            return getAllWorkshopsResponse();
        }

        public HandlerResponse getAllWorkshopsResponse()
        {
            String formattedXml = prettyPrinter();
            return new HandlerResponse(new StringBuilder(formattedXml), ALL_WORKSHOPS_STYLESHEET);
        }

        private string prettyPrinter()
        {
            XMLBuilder allWorkshopsXml = new XMLBuilder("workshops");
            WorkshopRepository repository = workshopManager().getWorkshopRepository();

            foreach (string id in repository.Workshop.Keys)
            {
                Workshop workshop = repository.getWorkshop(id);
                allWorkshopsXml.addBelowParent("workshop");
                allWorkshopsXml.addAttribute("id", workshop.ID);
                allWorkshopsXml.addAttribute("name", workshop.Name);
                allWorkshopsXml.addAttribute("status", workshop.Status);
                allWorkshopsXml.addAttribute("duration", workshop.getDurationAsString());
            }
            String formattedXml = getFormattedData(allWorkshopsXml.ToString());
            return formattedXml;
        }

        private string getFormattedData(string p)
        {
            throw new NotImplementedException();
        }
        private WorkshopManager workshopManager()
        {
            return catalogApp.WorkshopManager;
        }
    }

    public class NewWorkshopResponse:Handler
    {
        public NewWorkshopResponse(CatalogApp catalogApp):base(catalogApp){}

        public override HandlerResponse execute(Dictionary<string, string> parameters)
        {
            return getNewWorkshopResponse(parameters);
        }

        public HandlerResponse getNewWorkshopResponse(Dictionary<string, string> parameters)
        {
            createNewWorkshop(parameters);
            return catalogApp.executeActionAndGetResponse(catalogApp.ALL_WORKSHOPS, parameters);
        }

        private void createNewWorkshop(Dictionary<string, string> parameters)
        {
            String nextWorkshopID = workshopManager().NextWorkshopID;
            workshopManager().addWorkshop(newWorkshopContents(nextWorkshopID));
            parameters.Add("id", nextWorkshopID);
        }

        private StringBuilder newWorkshopContents(String nextWorkshopID)
        {
            StringBuilder newWorkshopContents = 
                workshopManager().createNewFileFromTemplate(nextWorkshopID, workshopManager().WorkshopDir, workshopManager().WorkshopTemplate);
            return newWorkshopContents;
        }

        private WorkshopManager workshopManager()
        {
            return catalogApp.WorkshopManager;
        }
    }
    #endregion
}