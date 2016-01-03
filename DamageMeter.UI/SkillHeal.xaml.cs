﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DamageMeter.Skills.Skill;
using DamageMeter.Skills.Skill.SkillDetail;

namespace DamageMeter.UI
{
    /// <summary>
    ///     Logique d'interaction pour Skill.xaml
    /// </summary>
    public partial class SkillHeal : ISkill
    {
        public SkillHeal(Skill skill, SkillStats stats)
        {
            InitializeComponent();

            LabelName.Content = skill.SkillName;
            Update(skill, stats);
        }

        public void Update(Skill skill, SkillStats stats)
        {
            var skillsId = "";
            for (var i = 0; i < skill.SkillId.Count; i++)
            {
                skillsId += skill.SkillId[i];
                if (i < skill.SkillId.Count - 1)
                {
                    skillsId += ",";
                }
            }

            LabelId.Content = skillsId;
            LabelCritRateHeal.Content = stats.CritRateHeal + "%";


            LabelNumberHitHeal.Content = stats.HitsHeal;
            LabelNumberCritHeal.Content = stats.CritsHeal;

            LabelTotalHeal.Content = FormatHelpers.Instance.FormatValue(stats.Heal);
            LabelBiggestHit.Content = FormatHelpers.Instance.FormatValue(stats.HealBiggestHit);
            LabelBiggestCrit.Content = FormatHelpers.Instance.FormatValue(stats.HealBiggestCrit);


            LabelAverageCrit.Content = FormatHelpers.Instance.FormatValue(stats.HealAverageCrit);
            LabelAverageHit.Content = FormatHelpers.Instance.FormatValue(stats.HealAverageHit);
            LabelAverage.Content = FormatHelpers.Instance.FormatValue(stats.HealAverageTotal);


            IEnumerable<KeyValuePair<int, SkillDetailStats>> listStats = stats.SkillDetails.ToList();
            listStats = listStats.OrderByDescending(stat => stat.Value.Damage);
            SkillsDetailList.Items.Clear();
            foreach (var stat in listStats)
            {
                SkillsDetailList.Items.Add(new SkillDetail(stat.Value));
            }
        }

        public string SkillNameIdent()
        {
            return (string) LabelName.Content;
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            var w = Window.GetWindow(this);
            try
            {
                w?.DragMove();
            }
            catch
            {
                Console.WriteLine(@"Exception move");
            }
        }

        private void UIElement_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Background = Brushes.Transparent;
        }

        private void UIElement_OnMouseEnter(object sender, MouseEventArgs e)
        {
            Background = Brushes.Black;
        }
    }
}