using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : Controller
    {
        List<CatalogItem> ci;
        public CatalogController()
        {
            ///Created();
            ci = Restored();
        }
        public void Created()
        {
            ci = new List<CatalogItem>()
            {
                new CatalogItem() { Number = 89011342334, Name = "Kevin", SecondName = "&"},
                new CatalogItem() { Number = 89011342335, Name = "Rowan", SecondName = "/"},
                new CatalogItem() { Number = 89011342336, Name = "Becca", SecondName = "?"}
            };
            using (var file = new FileStream("File.txt", FileMode.Create))
            {
                var xml = new XmlSerializer(typeof(List<CatalogItem>), new Type[] { typeof(CatalogItem) });
                xml.Serialize(file, ci);
            }
        }
        public List<CatalogItem> Restored()
        {
            using (var file = new FileStream("File.txt", FileMode.Open))
            {
                var xml = new XmlSerializer(typeof(List<CatalogItem>), new Type[] { typeof(CatalogItem) });
                var tasks = (List<CatalogItem>)xml.Deserialize(file);
                return tasks;
            }
        }
        [HttpGet]
        public IEnumerable<CatalogItem> Get()
        {
            return ci.ToArray();
        }
        [HttpPost]
        public IActionResult Post(CatalogItem item)
        {
            if (item == null)
            {
                throw new ArgumentException("item is null");
            }
            ci.Add(item);
            using (var file = new FileStream("File.txt", FileMode.Create))
            {
                var xml = new XmlSerializer(typeof(List<CatalogItem>), new Type[] { typeof(CatalogItem) });
                xml.Serialize(file, ci);
            }
            return Ok(item);
        }
        [HttpPut]
        public IActionResult Put(CatalogItem item)
        {
            if (item == null)
            {
                throw new ArgumentException("item is null");
            }
            if (!ci.Any(x => x.Number == item.Number))
            {
                return NotFound();
            }
            CatalogItem item2 = ci.FirstOrDefault(x => x.Number == item.Number);
            item2.Name = item.Name;
            item2.SecondName = item.SecondName;
            using (var file = new FileStream("File.txt", FileMode.Create))
            {
                var xml = new XmlSerializer(typeof(List<CatalogItem>), new Type[] { typeof(CatalogItem) });
                xml.Serialize(file, ci);
            }
            return Ok(item);
        }
        [HttpDelete("{number}")]
        public IActionResult Delete(long number)
        {
            if (ci == null)
            {
                return NotFound();
            }
            CatalogItem deleted = ci.FirstOrDefault(x => x.Number == number);
            ci.Remove(deleted);
            using (var file = new FileStream("File.txt", FileMode.Create))
            {
                var xml = new XmlSerializer(typeof(List<CatalogItem>), new Type[] { typeof(CatalogItem) });
                xml.Serialize(file, ci);
            }
            return Ok(ci);
        }
    }
}
