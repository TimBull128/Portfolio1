using BB2020MVC.Controllers;
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
    public interface IBusinessRace
    {
        IList<Race> GetRacesBySpecialRule(int SRID);
        SelectList CreateSelectListPlayerSkills(int PlayerID);
        SelectList CreateSelectListSpecialRules();
        SelectList SLSkills();
        RaceVM AddRace(Race NewRace);
        void DeleteRace(int RaceID);
        void DelPlayer(int PlayerID);
        SelectList SelectListMA();
        SelectList SelectListST();
        SelectList SelectListAG();
        SelectList SelectListPA();
        SelectList SelectListAV();
        Race GetRaceByPlayerID(int PlayerID);
        IList<SelectListItem> ValidSkills(int PlayerID);
        PlayerVM GetPlayerVM(int PlayerID);
        RaceVM GetRaceVM(int RaceID);
    }
    public class BusinessRace : IBusinessRace
    {
        private readonly ISRaces _RacesRepos;
        private readonly ISRaceSpecialRules _RaceSpecRepos;
        private readonly ISPlayerSkills _RacesPlayerSkills_Repos;
        private readonly ISPlayers _RacesPlayers_Repos;
        private readonly ISPlayerSkillTypes _RacesPlayerSkillTypes_Repos;
        private readonly ISSkills _RulesSkills_Repos;
        private readonly ISSpecialRules _RulesSpecialRules_Repos;
        private readonly IDefaults _Defaults_Repos;
        private readonly ISSkillsTypes _SkillTypes_Repos;
        public BusinessRace() : this(new ServiceLayer.Races(), 
            new RaceSpecialRules(), 
            new ServiceLayer.PlayerSkills(), 
            new ServiceLayer.PlayerSkillTypes(), 
            new ServiceLayer.Players(), 
            new Skills(), 
            new ServiceLayer.SpecialRules(), 
            new Default(),
            new ServiceLayer.SkillTypes()) { }
        public BusinessRace(ISRaces RaceRepos,
            ISRaceSpecialRules RaceSpecRepos,
            ISPlayerSkills RacesPlayerSkills_Repos,
            ISPlayerSkillTypes RacesPlayerSkillTypes_Repos,
            ISPlayers RacesPlayers_Repos,
            ISSkills rulesSkills_Repos, 
            ISSpecialRules SpecialRules_Repos, 
            IDefaults Defaults_Repos,
            ISSkillsTypes RulesSkillTypes_Repos)
        {
            _RacesRepos = RaceRepos;
            _RaceSpecRepos = RaceSpecRepos;
            _RacesPlayers_Repos = RacesPlayers_Repos;
            _RacesPlayerSkills_Repos = RacesPlayerSkills_Repos;
            _RulesSkills_Repos = rulesSkills_Repos;
            _RulesSpecialRules_Repos = SpecialRules_Repos;
            _Defaults_Repos = Defaults_Repos;
            _RacesPlayerSkillTypes_Repos = RacesPlayerSkillTypes_Repos;
            _SkillTypes_Repos = RulesSkillTypes_Repos;
        }

        public IList<Race> GetRacesBySpecialRule(int SRID)
        {
            return (from Races in _RacesRepos.Read()
                    from Races_SpecialRules in _RaceSpecRepos.All(SRID)
                    where Races.RaceID == Races_SpecialRules.RaceID
                    select Races).ToList();
        }
        public SelectList CreateSelectListPlayerSkills(int PlayerID)
        {
            IList<Races_Players_Skills> PlayerSkillsList = _RacesPlayerSkills_Repos.All(PlayerID);
            IList<SelectListItem> ResultSelectList = new List<SelectListItem>();

            foreach (var item in PlayerSkillsList)
            {
                Rules_Skills_List SelectedSkill = _RulesSkills_Repos.GetSingle(item.SkillID);
                ResultSelectList.Add(new SelectListItem { Value = SelectedSkill.SkillID.ToString(), Text = SelectedSkill.Name });

            }
            ResultSelectList.Add(new SelectListItem { Value = "-1", Text = "-" });

            return new SelectList(ResultSelectList, "value", "Text");

        }
        public SelectList CreateSelectListSpecialRules()
        {
            IList<SelectListItem> SelectList = new List<SelectListItem>();

            foreach (var item in _RulesSpecialRules_Repos.Read())
            {
                SelectList.Add(new SelectListItem { Value = item.SRID.ToString(), Text = item.Name });
            }

            return new SelectList(SelectList, "Value", "Text");
        }
        public SelectList SLSkills()
        {
            IList<SelectListItem> SelectList = new List<SelectListItem>();
            foreach (var Item in _RulesSkills_Repos.All())
            {
                SelectList.Add(new SelectListItem { Value = Item.SkillID.ToString(), Text = Item.Name.ToString() });
            }
            return new SelectList(SelectList, "Value", "Text");
        }
        public RaceVM AddRace(Race NewRace)
        {
            var race = _RacesRepos.Create(NewRace);
            for (int i = 1; i <= 3; i++)
            {
                _RaceSpecRepos.Create(
                    _Defaults_Repos.Races_Special_Rules(race.RaceID)
                );
            }
            return GetRaceVM(NewRace.RaceID);
        }
        public void DeleteRace(int RaceID)
        {
            _RacesPlayers_Repos.DeleteMulti(_RacesPlayers_Repos.All(RaceID));
            _RaceSpecRepos.DeleteMulti(_RaceSpecRepos.All(RaceID));
            _RacesRepos.Delete(_RacesRepos.GetSingle(RaceID));
        }
        public void DelPlayer(int PlayerID)
        {
            _RacesPlayerSkills_Repos.DeleteMulti(_RacesPlayerSkills_Repos.All(PlayerID));
            _RacesPlayerSkillTypes_Repos.DeleteMulti(_RacesPlayerSkillTypes_Repos.All(PlayerID));
            _RacesPlayers_Repos.Delete(_RacesPlayers_Repos.GetSingle(PlayerID));
        }
        public SelectList SelectListMA()
        {
            IList<SelectListItem> List = new List<SelectListItem>();
            SelectListItem Item;
            for (int i = 1; i <= 9; i++)
            {
                Item = new SelectListItem()
                {
                    Value = i.ToString(),
                    Text = i.ToString()
                };
                List.Add(Item);
            }

            return new SelectList(List, "Value", "Text");
        }
        public SelectList SelectListST()
        {
            IList<SelectListItem> List = new List<SelectListItem>();
            SelectListItem Item;
            for (int i = 1; i <= 8; i++)
            {
                Item = new SelectListItem
                {
                    Value = i.ToString(),
                    Text = i.ToString(),

                };
                List.Add(Item);
            }
            return new SelectList(List, "Value", "Text");
        }
        public SelectList SelectListAG()
        {
            IList<SelectListItem> List = new List<SelectListItem>();
            SelectListItem Item;
            for (int i = 6; i >= 1; i--)
            {
                Item = new SelectListItem
                {
                    Value = i.ToString(),
                    Text = i.ToString() + "+",

                };
                List.Add(Item);
            }
            return new SelectList(List, "Value", "Text");

        }
        public SelectList SelectListPA()
        {
            IList<SelectListItem> List = new List<SelectListItem>();
            SelectListItem Item;
            for (int i = 7; i >= 1; i--)
            {
                Item = new SelectListItem
                {
                    Value = i.ToString(),
                    Text = (i == 7) ? "-" : i.ToString() + "+"

                };
                List.Add(Item);
            }
            return new SelectList(List, "Value", "Text");

        }
        public SelectList SelectListAV()
        {
            IList<SelectListItem> List = new List<SelectListItem>();
            SelectListItem Item;
            for (int i = 3; i <= 11; i++)
            {
                Item = new SelectListItem
                {
                    Value = i.ToString(),
                    Text = i.ToString() + "+"
                };
                List.Add(Item);
            }
            return new SelectList(List, "Value", "Text");
        }
        public Race GetRaceByPlayerID(int PlayerID)
        {
            return _RacesRepos.GetSingle(_RacesPlayers_Repos.GetSingle(PlayerID).RaceID);
        }
        public IList<SelectListItem> ValidSkills(int PlayerID)
        {
            var SkillList =
                (from SQ in _RulesSkills_Repos.All()
                 from PQ in _RacesPlayerSkills_Repos.All(PlayerID)
                 where SQ.SkillID != PQ.SkillID
                 select SQ).ToList();
            IList<SelectListItem> SkillSelect = new List<SelectListItem>();
            foreach (var SQ in SkillList)
            {
                SkillSelect.Add(
                        new SelectListItem() { Text = SQ.Name, Value = SQ.SkillID.ToString() }
                    );
            }
            return SkillSelect;
        }
        public PlayerVM GetPlayerVM(int PlayerID)
        {
            return new PlayerVM(_RacesPlayers_Repos.GetSingle(PlayerID), _RacesPlayerSkills_Repos, _SkillTypes_Repos, _RacesPlayerSkillTypes_Repos);
        }
        public RaceVM GetRaceVM(int RaceID)
        {

            IList<PlayerVM> PlayerList = new List<PlayerVM>();

            foreach (var player in _RacesPlayers_Repos.All(RaceID))
            {
                PlayerList.Add(
                    GetPlayerVM(player.PlayerID)
                );
            }

            return new RaceVM(_RacesRepos.GetSingle(RaceID), PlayerList, _RulesSpecialRules_Repos, _RaceSpecRepos);
        }
    }
}