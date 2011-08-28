﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using ContentHunter.Web.Models.Engines;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ContentHunter.Web.Models
{
    public class Instruction: ICloneable
    {
        public Instruction()
        {
            Type = GetType(InputType.Html);
            IsOriginal = true;
        }

        public enum InputType : short {Rss, Html, Xml}

        public static short GetType(InputType type)
        {
            return (short)type;
        }

        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Url { get; set; }
        
        [Required]
        public short Type { get; set; }

        [Required, StringLength(45)]
        public string Engine { get; set; }
        
        [Required]
        public bool IsRecursive { get; set; }
        
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        
        [Required]
        public bool IsRecurrent { get; set; }
        
        //used on lucene index
        [StringLength(50)]
        public string Category { get; set; }

        //used to recursive crawler, do not persist on database
        [NotMapped]
        public bool IsOriginal { get; set; }

        private Crawler GetEngine()
        {
            Crawler crawler = (Crawler)Assembly.GetExecutingAssembly().CreateInstance(string.Format("ContentHunter.Web.Models.Engines.{0}", Engine));
            crawler.Input = this;
            return crawler;
        }

        public List<CrawlerResult> Execute()
        {
            return GetEngine().Execute();
        }


        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}