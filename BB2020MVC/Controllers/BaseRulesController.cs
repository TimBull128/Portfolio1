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
            IList<Rules_Skills_Types> SkillTypes = _RulesRepository.GetAllRulesSkillTypes();
            
            ViewBag.SkillTypes = SkillTypes;
            return View(SkillList);
        }

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

            _RulesRepository.DeleteSkill(Skill.SkillID);

            return RedirectToAction("AllSkills");

        }

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
                ViewBag.SkillTypes = _RulesRepository.CreateSelectListSkillTypes();
                return View(Skill);
            }

        }

        public ActionResult EditSkill(int ID)
        {

            Rules_Skills_List Skill = _RulesRepository.GetSkill(ID);
            ViewBag.SkillTypes = _RulesRepository.CreateSelectListSkillTypes();
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
                ViewBag.SkillTypes = _RulesRepository.CreateSelectListSkillTypes();
                return View(Skill);
            }
        }

        public ActionResult AllSkillTypes()
        {
            IList<Rules_Skills_Types> SkillList = _RulesRepository.GetAllRulesSkillTypes();
            return View(SkillList);
        }
        public ActionResult AddSkillType()
        {
            Rules_Skills_Types NewSkillType = _RulesRepository.GetNewSkillTypeBase();
            return View(NewSkillType);
        }
        [HttpPost]
        public ActionResult AddSkillType(Rules_Skills_Types GatheredSkillType)
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
            Rules_Skills_Types SelectedRule = _RulesRepository.GetSkillType(ID);
            return View(SelectedRule);
        }
        [HttpPost]
        public ActionResult EditSkillType(Rules_Skills_Types GatheredRule)
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
            Rules_Skills_Types SelectRules = _RulesRepository.GetSkillType(ID);
            return View(SelectRules);
        }
        [HttpPost]
        public ActionResult DeleteSkillType(Rules_Skills_Types SkillType)
        {
            _RulesRepository.DeleteSkillType(SkillType.STypeID);
            return RedirectToAction("AllSkillTypes");
        }

        public ActionResult AllSpecialRules()
        {
            IList<Rules_SpecialRules> SpecialRulesList = _RulesRepository.GetAllSpecialRules();

            return View(SpecialRulesList);
        }


        public ActionResult AddSpecialRule()
        {

            var NewSR = _RulesRepository.GetNewSpecialRuleBase();
            return View(NewSR);
        }
        [HttpPost]
        public ActionResult AddSpecialRule(Rules_SpecialRules SpecialRule) {

            _RulesRepository.AddSpecialRule(SpecialRule);
            return RedirectToAction("AllSpecialRules");

        }


  
        public ActionResult DeleteSpecialRule(int ID)
        {
            Rules_SpecialRules SpecialRule = _RulesRepository.GetSpecialRule(ID);
            return View(SpecialRule);
        }
        [HttpPost]
        public ActionResult DeleteSpecialRule(Rules_SpecialRules SpecialRule)
        {

            _RulesRepository.DeleteSpecialRule(SpecialRule.SRID);
            return RedirectToAction("AllSpecialRules");
        }
        public ActionResult EditSpecialRule(int ID)
        {
            Rules_SpecialRules SpecialRule = _RulesRepository.GetSpecialRule(ID);
            return View(SpecialRule);
        }
        [HttpPost]
        public ActionResult EditSpecialRule(Rules_SpecialRules SpecialRule)
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

        
        public ActionResult AllInjuryTypes()
        {
            IList<Rules_InjuryTypes> AllInjuryTypes = _RulesRepository.GetAllInjuryTypes();
            return View(AllInjuryTypes);
        }
        public ActionResult AddInjuryType()
        {
            Rules_InjuryTypes InjuryType = _RulesRepository.GetNewInjuryTypesBase();
            return View(InjuryType);
        }
        [HttpPost]
        public ActionResult AddInjuryType(Rules_InjuryTypes InjuryType)
        {
            if (ModelState.IsValid)
            {
                _RulesRepository.AddInjuryType(InjuryType);
                return RedirectToAction("AllInjuryTypes");
            }
            else
            {
                return View(InjuryType);
            }
        }
        public ActionResult DeleteInjuryType(int ID)
        {
            Rules_InjuryTypes InjuryType = _RulesRepository.GetInjuryType(ID);
            return View(InjuryType);
        }
        [HttpPost]
        public ActionResult DeleteInjuryType(Rules_InjuryTypes Injury)
        {
            _RulesRepository.DeleteInjuryType(Injury.InjuryID);
            return RedirectToAction("AllInjuryTypes");
        }
        //TODO: TEST all below
        public ActionResult ViewForbiddenSkills(int SkillID)
        {
            var SelectedSkill = _RulesRepository.GetSkill(SkillID);

            FSkillsVM Model = new FSkillsVM()
            {
                Skill = SelectedSkill,
                SkillList = _RulesRepository.GetAllSkills(),
                FSkillList = _RulesRepository.GetFSkillListforSkill(SelectedSkill.SkillID)
            };
            return View(Model);
        }

        public ActionResult AddForbiddenSkill(int SkillID)
        {
            AddFSkillVM Model = new AddFSkillVM()
            {
                FSkill = _RulesRepository.GetNewFSkillBase(SkillID),
                Skill = _RulesRepository.GetSkill(SkillID),
                List = _RulesRepository.CreateFSkillList(SkillID)
            };

            return View(Model);
            
        }
        [HttpPost]
        public ActionResult AddForbiddenSkill(AddFSkillVM Model)
        {
            _RulesRepository.AddForbiddenSkill(Model.FSkill);
            return RedirectToAction("ViewForbiddenSkills", new { Model.Skill.SkillID });
        }
        public ActionResult DeleteForbiddenSkill(int FSKillID)
        {
            var FSkill = _RulesRepository.GetFSkillBase(FSKillID);


            DeleteFSkillVM Model = new DeleteFSkillVM()
            {
                Skill = _RulesRepository.GetSkill(FSkill.SkillID),
                FSkillDetail = FSkill,
                FSkill = _RulesRepository.GetSkill(FSkill.FSkillID)
            };
            return View(Model);           
        }
        [HttpPost]
        public ActionResult DeleteForbiddenSkill(DeleteFSkillVM Model)
        {
            
            _RulesRepository.DeleteForbiddenSkill(Model.FSkillDetail.ForbiddenID);
            return RedirectToAction("ViewForbiddenSkills", new { Model.Skill.SkillID });
        }

        
    }
}