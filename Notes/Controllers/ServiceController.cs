using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Notes.Models;

namespace Notes.Controllers
{
    public class ServiceController : Controller
    {
        public ActionResult Index()
        {
            XmlDocument XmlDocObj = new XmlDocument();
            XmlDocObj.Load(Server.MapPath("~/Services.xml"));
            XmlNode RootNode = XmlDocObj.SelectSingleNode("Services");
            XmlNodeList Services = RootNode.ChildNodes;
            List<Service> ServicesList = new List<Service>();
            foreach (XmlNode node in Services)
            {
                if (node != null)
                    ServicesList.Add(new Service
                    {
                        ServiceName = node.SelectSingleNode("ServiceName").InnerText,
                        id = node.SelectSingleNode("id").InnerText,
                        ImageUrl = node.SelectSingleNode("ImageUrl").InnerText,
                        Description =  node.SelectSingleNode("Description").InnerText,
                        CreatedBy = node.SelectSingleNode("CreatedBy").InnerText,
                        CreatedDate = node.SelectSingleNode("CreatedDate").InnerText,
                        IsActive = node.SelectSingleNode("IsActive").InnerText

                    });
            }
            return View(ServicesList);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Service note)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(Server.MapPath("~/Services.xml"));
            XmlNode RootNode = XmlDoc.SelectSingleNode("Services");
            XmlNodeList notesList = RootNode.ChildNodes;

            XmlNode noteNode = RootNode.AppendChild(XmlDoc.CreateNode(XmlNodeType.Element, "note", ""));

            noteNode.AppendChild(XmlDoc.CreateNode(XmlNodeType.Element, "id", "")).InnerText = new Random().Next(0, int.MaxValue).ToString();
            noteNode.AppendChild(XmlDoc.CreateNode(XmlNodeType.Element, "ServiceName", "")).InnerText = note.ServiceName;
            noteNode.AppendChild(XmlDoc.CreateNode(XmlNodeType.Element, "ImageUrl", "")).InnerText = note.ImageUrl;
            noteNode.AppendChild(XmlDoc.CreateNode(XmlNodeType.Element, "Description", "")).InnerText = note.Description;
            noteNode.AppendChild(XmlDoc.CreateNode(XmlNodeType.Element, "CreatedBy", "")).InnerText = note.CreatedBy;
            noteNode.AppendChild(XmlDoc.CreateNode(XmlNodeType.Element, "CreatedDate", "")).InnerText = note.CreatedDate;
            noteNode.AppendChild(XmlDoc.CreateNode(XmlNodeType.Element, "IsActive", "")).InnerText = note.IsActive;


            XmlDoc.Save(Server.MapPath("~/Services.xml"));


            return RedirectToAction("Index");

        }
        public ActionResult Edit(String id)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(Server.MapPath("~/Services.xml"));
            XmlNode RootNode = XmlDoc.SelectSingleNode("Services");
            XmlNodeList Services = RootNode.ChildNodes;

            XmlNode service = Services[0];

            foreach (XmlNode n in Services)
            {
                if (n["id"].InnerText == id)
                {
                    service = n;
                }
            }
            Service note_object = new Service {
                ServiceName = service.SelectSingleNode("ServiceName").InnerText,
                id = service.SelectSingleNode("id").InnerText,
                ImageUrl = service.SelectSingleNode("ImageUrl").InnerText,
                Description = service.SelectSingleNode("Description").InnerText,
                CreatedBy = service.SelectSingleNode("CreatedBy").InnerText,
                CreatedDate = service.SelectSingleNode("CreatedDate").InnerText,
                IsActive = service.SelectSingleNode("IsActive").InnerText

            };

            return View(note_object);

        }
        public ActionResult Update(Service n)
        {
            XmlDocument XmlDocObj = new XmlDocument();
            XmlDocObj.Load(Server.MapPath("~/Services.xml"));
            XmlNode RootNode = XmlDocObj.SelectSingleNode("Services");
            XmlNodeList Services = RootNode.ChildNodes;

            XmlNode service = Services[0];

            foreach (XmlNode ns in Services)
            {
                if (ns["id"].InnerText == n.id.ToString())
                {
                    service = ns;
                }
            }

            service["ServiceName"].InnerText = n.ServiceName;
            service["ImageUrl"].InnerText = n.ImageUrl;
            service["Description"].InnerText = n.Description;
            service["CreatedBy"].InnerText = n.CreatedBy;
            service["CreatedDate"].InnerText = n.CreatedDate;
            service["IsActive"].InnerText = n.IsActive;
            

            XmlDocObj.Save(Server.MapPath("~/Services.xml"));

            return RedirectToAction("Index");

        }

        public ActionResult Delete(String id)
        {
            XmlDocument XmlDocObj = new XmlDocument();
            XmlDocObj.Load(Server.MapPath("~/Services.xml"));
            XmlNode RootNode = XmlDocObj.SelectSingleNode("Services");
            XmlNodeList Services = RootNode.ChildNodes;

            XmlNode note = Services[0];

            foreach (XmlNode n in Services)
            {
                if (n["id"].InnerText == id)
                {
                    note = n;
                }
            }

            note.RemoveAll();
            RootNode.RemoveChild(note);

            XmlDocObj.Save(Server.MapPath("~/Services.xml"));

            return RedirectToAction("Index");
        }
    }
}