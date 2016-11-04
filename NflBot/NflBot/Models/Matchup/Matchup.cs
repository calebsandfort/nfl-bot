using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace NflBot.Models
{
    [DataContract]
    public class Matchup
    {
        public String Key
        {
            get
            {
                return $"{AwayTeam}.{HomeTeam}";
            }
        }

        public DateTime WhenDate { get; set; }

        [DataMember]
        public String When {
            get
            {
                return this.WhenDate.ToString("ddd MMM d 'at' h:mm tt 'ET'");
            }
        }

        [DataMember]
        public String Where { get; set; }

        public Networks NetworkEnum { get; set; }

        [DataMember]
        public String Network
        {
            get
            {
                return this.NetworkEnum.GetAttribute<DisplayAttribute>().Display;
            }
        }

        public Teams AwayTeamEnum { get; set; }

        [DataMember]
        public String AwayTeam
        {
            get
            {
                return this.AwayTeamEnum.GetAttribute<DisplayAttribute>().Display;
            }
        }

        public Teams HomeTeamEnum { get; set; }

        [DataMember]
        public String HomeTeam
        {
            get
            {
                return this.HomeTeamEnum.GetAttribute<DisplayAttribute>().Display;
            }
        }
    }
}