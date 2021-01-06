using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BB2020MVC.Models;
//foreach (var keys in FC.AllKeys)
//{
//    Response.Write( "Key : " + keys + "<br/>");
//    Response.Write("Value: " + FC[keys] + "<br/>");
//}
//return View("Test", GatheredPlayer);

namespace BB2020MVC.Controllers
{
    public class BaseRulesController : Controller
    {
        private readonly IRulesRepos _RulesRepository;

        public BaseRulesController() : this(new ModelRules()) { }


        public BaseRulesController(IRulesRepos _Repository)
        {
            _RulesRepository = _Repository;
        }
        // GET: BaseRules
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AllSkills()
        {
            IList<Rules_Skills_List> SkillList = _RulesRepository.GetAllSkills();
            IList<Rules_Skills_Type> SkillTypes = _RulesRepository.GetAllRulesSkillTypes();
            ViewBag.SkillTypes = SkillTypes;
            return View(SkillList);
        }
        //ToDo: Test when Skill added
        public ActionResult DeleteSkill(int ID)
        {
            var SelectedSkill = _RulesRepository.GetSkill(ID);
            var SkillType = _RulesRepository.GetSkillsByType(SelectedSkill.SkillTypeID);
            ViewBag.SkillType = SkillType;
            return View(SelectedSkill);
        }
        [HttpPost]
        public ActionResult DeleteSkill(Rules_Skills_List Skill)
        {

            _RulesRepository.DeleteSkill(Skill.ID);

            return RedirectToAction("AllSkills");

        }

        //ToDo: Test when SkillTypes are added
        public ActionResult AddSkill()
        {
            Rules_Skills_List NewSkill = _RulesRepository.GetNewSkillBase();
            ViewBag.SkillTypes = _RulesRepository.CreateSelectListSkillTypes();
            return View(NewSkill);
        }
        [HttpPost]
        public ActionResult AddSkill(Rules_Skills_List Skill)
        {
            if (ModelState.IsValid)
            {
                _RulesRepository.AddSkill(Skill);
                return RedirectToAction("AllSkills");
            }
            else
            {
                ViewBag.SkillTypes = _RulesRepository.CreateSelectListSkillTypes(Skill.SkillTypeID);
                return View(Skill);
            }

        }
        //ToDo: Test when SkillTypes and skill is added
        public ActionResult EditSkill(int ID)
        {

            Rules_Skills_List Skill = _RulesRepository.GetSkill(ID);
            ViewBag.SkillTypes = _RulesRepository.CreateSelectListSkillTypes(Skill.SkillTypeID);
            return View(Skill);
        }
        [HttpPost]
        public ActionResult EditSkill(Rules_Skills_List Skill)
        {
            if (ModelState.IsValid)
            {
                _RulesRepository.EditSkill(Skill);
                return RedirectToAction("AllSkills");
            }
            else
            {
                ViewBag.SkillTypes = _RulesRepository.CreateSelectListSkillTypes(Skill.SkillTypeID);
                return View(Skill);
            }
        }

        public ActionResult AllSkillTypes()
        {
            IList<Rules_Skills_Type> SkillList = _RulesRepository.GetAllRulesSkillTypes();
            return View(SkillList);
        }
        public ActionResult AddSkillType()
        {
            Rules_Skills_Type NewSkillType = _RulesRepository.GetNewSkillTypeBase();
            return View(NewSkillType);
        }
        [HttpPost]
        public ActionResult AddSkillType(Rules_Skills_Type GatheredSkillType)
        {
            if (ModelState.IsValid)
            {
                _RulesRepository.AddSkillType(GatheredSkillType);
                return RedirectToAction("AllSkillTypes");
            }
            else
            {
                return View(GatheredSkillType);
            }

        }
        public ActionResult EditSkillType(int ID)
        {
            Rules_Skills_Type SelectedRule = _RulesRepository.GetSkillType(ID);
            return View(SelectedRule);
        }
        [HttpPost]
        public ActionResult EditSkillType(Rules_Skills_Type GatheredRule)
        {
            if (ModelState.IsValid)
            {
                _RulesRepository.EditSkillType(GatheredRule);
                return RedirectToAction("AllSkillTypes");
            }
            else
            {
                return View(GatheredRule);
            }
        }
        public ActionResult DeleteSkillType(int ID)
        {
            Rules_Skills_Type SelectRules = _RulesRepository.GetSkillType(ID);
            return View(SelectRules);
        }
        [HttpPost]
        public ActionResult DeleteSkillType(Rules_Skills_Type SkillType)
        {
            _RulesRepository.DeleteSkillType(SkillType.ID);
            return RedirectToAction("AllSkillTypes");
        }
        /// <summary>
        /// Special Rules
        /// </summary>
        /// <returns></returns>
        //List All Special rules
        public ActionResult AllSpecialRules()
        {
            IList<Rules_SpecialRule> SpecialRulesList = _RulesRepository.GetAllSpecialRules();

            return View(SpecialRulesList);
        }

        //Add Special Rule
        public ActionResult AddSpecialRule()
        {
            return View();
        }
        [HttpPost]

        public ActionResult AddSpecialRule(Rules_SpecialRule SpecialRule) {
            if (ModelState.IsValid)
            {
                _RulesRepository.AddSpecialRule(SpecialRule);
                return RedirectToAction("AllSpecialRules");
            }
            else
            {
                return View(SpecialRule);
            }
        }


        ///Delete Special Rule
        public ActionResult DeleteSpecialRule(int ID)
        {
            Rules_SpecialRule SpecialRule = _RulesRepository.GetSpecialRule(ID);
            return View(SpecialRule);
        }
        [HttpPost]
        public ActionResult DeleteSpecialRule(Rules_SpecialRule SpecialRule)
        {

            _RulesRepository.DeleteSpecialRule(SpecialRule.ID);
            return RedirectToAction("AllSpecialRules");
        }
        public ActionResult EditSpecialRule(int ID)
        {
            Rules_SpecialRule SpecialRule = _RulesRepository.GetSpecialRule(ID);
            return View(SpecialRule);
        }
        [HttpPost]
        public ActionResult EditSpecialRule(Rules_SpecialRule SpecialRule)
        {
            if (ModelState.IsValid)
            {
                _RulesRepository.EditSpecialRule(SpecialRule);
                return RedirectToAction("AllSpecialRules");
            }
            else
            {
                return View(SpecialRule);
            }
        }
    }
}