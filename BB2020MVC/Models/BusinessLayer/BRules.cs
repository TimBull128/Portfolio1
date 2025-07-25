using BB2020MVC.Controllers;
using BB2020MVC.Models.DataLayer.Races;
using BB2020MVC.Models.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BB2020MVC.Models.BusinessLayer
{
    public interface IBusinessRules
    {

    }
    public class BusinessRules : IBusinessRules
    {
        internal ISSkillsTypes _SkillTypes_repo;
        public BusinessRules() : this(new ServiceLayer.SkillTypes()) { }
        public BusinessRules(ISSkillsTypes skillTypes_repo)
        {
            _SkillTypes_repo = skillTypes_repo;
        }

        public IList<SelectListItem> CreateSelectListSkillTypes()
        {
            SelectListItem ThisSelectListItem;
            IList<SelectListItem> SelectListItems = new List<SelectListItem>();
            var SkillTypes = _SkillTypes_repo.All();
            foreach (var SkillType in SkillTypes)
            {
                ThisSelectListItem = new SelectListItem()
                {
                    Text = SkillType.Name,
                    Value = SkillType.STypeID.ToString()

                };
                SelectListItems.Add(ThisSelectListItem);
            }

            return SelectListItems;
        }
        public IList<SelectListItem> CreateSelectListSkills(int SelectedValue = -1)
        {
            SelectListItem NewSelectListItem;
            IList<SelectListItem> SelectListItems = new List<SelectListItem>();
            IList<Rules_Skills_List> Skills = GetAllSkills();
            foreach (var item in Skills)
            {
                NewSelectListItem = new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.SkillID.ToString(),
                    Selected = (item.SkillID == SelectedValue)
                };
                SelectListItems.Add(NewSelectListItem);
            }
            NewSelectListItem = new SelectListItem()
            {
                Text = "None",
                Value = "-1",
                Selected = (SelectedValue == -1)
            };
            SelectListItems.Add(NewSelectListItem);
            return SelectListItems;
        }
        public IList<SelectListItem> CreateSelectListLevelTypes(int SelectedValue = -1)
        {
            SelectListItem NewSelectListItem;
            IList<SelectListItem> SelectListItems = new List<SelectListItem>();
            IList<Rules_LvlType> Skills = GetAllLevelTypes();
            foreach (var item in Skills)
            {
                NewSelectListItem = new SelectListItem()
                {
                    Text = item.Description,
                    Value = item.ID.ToString(),
                    Selected = (item.ID == SelectedValue)
                };
                SelectListItems.Add(NewSelectListItem);
            }
            NewSelectListItem = new SelectListItem()
            {
                Text = "None",
                Value = "-1",
                Selected = (SelectedValue == -1)
            };
            SelectListItems.Add(NewSelectListItem);
            return SelectListItems;
        }
    }

}