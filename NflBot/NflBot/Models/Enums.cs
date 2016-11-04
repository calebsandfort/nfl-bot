using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NflBot.Models
{
    #region EnumExtensions
    public static class EnumExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return type.GetField(name) // I prefer to get attributes this way
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault();
        }

        public static bool FindTeamByNameVariation(this String value, out Teams team)
        {
            bool success = false;
            team = Teams.None;

            foreach (Teams t in Enum.GetValues(typeof(Teams)))
            {
                if (t.GetAttribute<DisplayVariationsAttribute>().DisplayVariations.Contains(value.ToLower()))
                {
                    team = t;
                    success = true;
                    break;
                }
            }

            return success;
        }

        public static bool FindNetworkByNameVariation(this String value, out Networks network)
        {
            bool success = false;
            network = Networks.None;

            foreach (Networks n in Enum.GetValues(typeof(Networks)))
            {
                if (n.GetAttribute<DisplayVariationsAttribute>().DisplayVariations.Contains(value.ToLower()))
                {
                    network = n;
                    success = true;
                    break;
                }
            }

            return success;
        }
    }
    #endregion

    #region DisplayVariationsAttribute
    public class DisplayVariationsAttribute : Attribute
    {
        internal DisplayVariationsAttribute(params string[] displayVariations)
        {
            this.DisplayVariations = displayVariations.ToList();
        }
        public List<String> DisplayVariations { get; private set; }
    }
    #endregion

    #region DisplayAttribute
    public class DisplayAttribute : Attribute
    {
        internal DisplayAttribute(string display)
        {
            this.Display = display;
        }
        public String Display { get; private set; }
    }
    #endregion

    #region Networks
    public enum Networks
    {
        [Display("None")]
        [DisplayVariations("none")]
        None = 0,

        [Display("NFL Network")]
        [DisplayVariations("nfln", "nfl network")]
        NflNetwork,

        [Display("CBS")]
        [DisplayVariations("cbs")]
        Cbs,

        [Display("FOX")]
        [DisplayVariations("fox")]
        Fox,

        [Display("NBC")]
        [DisplayVariations("nbc")]
        Nbc
    }
    #endregion

    #region Teams
    public enum Teams
    {
        [Display("None")]
        [DisplayVariations("none")]
        None = 0,

        [Display("Buffalo Bills")]
        [DisplayVariations("buffalo", "bills", "buffalo bills", "buf")]
        BuffaloBills,

        [Display("Miami Dolphins")]
        [DisplayVariations("miami", "fins", "dolphins", "miami fins", "miami dolphins", "ari")]
        MiamiDolphins,

        [Display("New England Patriots")]
        [DisplayVariations("new england", "pats", "patriots", "new england pats", "new england patriots", "ne")]
        NewEnglandPatriots,

        [DisplayVariations("new york jets", "jets", "nyj")]
        [Display("New York Jets")]
        NewYorkJets,

        [Display("Baltimore Ravens")]
        [DisplayVariations("baltimore", "ravens", "baltimore ravens", "bal")]
        BaltimoreRavens,

        [Display("Cincinnati Bengals")]
        [DisplayVariations("cincinnati", "cincy", "bengals", "cincinnati bengals", "cincy bengals", "cin")]
        CincinnatiBengals,

        [Display("Cleveland Browns")]
        [DisplayVariations("cleveland", "browns", "cleveland browns", "cle")]
        ClevelandBrowns,

        [Display("Pittsburgh Steelers")]
        [DisplayVariations("pittsburgh", "pitt", "steelers", "pittsburgh steelers", "pitt steelers", "pit")]
        PittsburghSteelers,

        [Display("Houstan Texans")]
        [DisplayVariations("houston", "texans", "houston texans", "hou")]
        HoustanTexans,

        [Display("Indianapolis Colts")]
        [DisplayVariations("indianapolis", "indy", "colts", "indianapolis colts", "indy colts", "ind")]
        IndianapolisColts,

        [Display("Jacksonville Jaguars")]
        [DisplayVariations("jacksonville", "jville", "jags", "jaguars", "jacksonville jags", "jacksonville jaguars", "jville jags", "jville jaguars", "jax")]
        JacksonvilleJaguars,

        [Display("Tennessee Titans")]
        [DisplayVariations("tennesse", "titans", "tennesse titans", "ten")]
        TennesseeTitans,

        [Display("None")]
        [DisplayVariations("denver", "broncos", "denver broncos", "den")]
        DenverBroncos,

        [Display("Kansas City Chiefs")]
        [DisplayVariations("kansas city", "chiefs", "kansas city chiefs", "kc")]
        KansasCityChiefs,

        [Display("Oakland Raiders")]
        [DisplayVariations("oakland", "raiders", "oakland raiders", "oak")]
        OaklandRaiders,

        [Display("San Diego Chargers")]
        [DisplayVariations("san diego", "chargers", "san diego chargers", "sd")]
        SanDiegoChargers,

        [Display("Dallas Cowboys")]
        [DisplayVariations("dallas", "cowboys", "dallas cowboys", "dal")]
        DallasCowboys,

        [Display("New York Giants")]
        [DisplayVariations("new york jets", "jets", "nyg")]
        NewYorkGiants,

        [Display("Philadelphia Eagles")]
        [DisplayVariations("philadelphia", "phila", "eagles", "philadelphia eagles", "phila eagles", "phi")]
        PhiladelphiaEagles,

        [Display("Washington Redskins")]
        [DisplayVariations("washington", "dc", "skins", "redskins", "washington skins", "washington redskins", "dc skins", "dc redskins", "was")]
        WashingtonRedskins,

        [Display("Chicago Bears")]
        [DisplayVariations("chicago", "bears", "chicago bears", "chi")]
        ChicagoBears,

        [Display("Detroit Lions")]
        [DisplayVariations("detroit", "lions", "detroit lions", "det")]
        DetroitLions,

        [Display("Green Bay Packers")]
        [DisplayVariations("green bay", "pack", "packers", "green bay pack", "green bay packers", "gb")]
        GreenBayPackers,

        [Display("Minnesota Vikings")]
        [DisplayVariations("minnesota", "minny", "vikes", "vikings", "minnesota vikes", "minnesota vikings", "minny vikes", "minny vikings")]
        MinnesotaVikings,

        [Display("Atlanta Falcons")]
        [DisplayVariations("atlanta", "falcons", "atlanta falcons", "atl")]
        AtlantaFalcons,

        [Display("Carolina Panthers")]
        [DisplayVariations("carolina", "panthers", "carolina panthers", "car")]
        CarolinaPanthers,

        [Display("None")]
        [DisplayVariations("new orleans", "saints", "new orleans saints", "no")]
        NewOrleansSaints,

        [Display("Tampa Bay Buccaneers")]
        [DisplayVariations("tampa bay", "tampa", "bucs", "buccaneers", "tampa bay bucs", "tampa bay buccaneers", "tampa bucs", "tampa buccaneers", "tb")]
        TampaBayBuccaneers,

        [Display("Arizona Cardinals")]
        [DisplayVariations("arizona", "zona", "cards", "cardinals", "arizona cards", "arizona cardinals", "zona cards", "zona cardinals")]
        ArizonaCardinals,

        [Display("Los Angeles Raiders")]
        [DisplayVariations("los angeles", "la", "rams", "los angeles rams", "la rams")]
        LosAngelesRaiders,

        [Display("San Francisco 49ers")]
        [DisplayVariations("san francisco", "san fran", "niners", "49ers", "san francisco niners", "san francisco 49ers", "san fran niners", "san fran 49ers", "sf")]
        SanFrancisco49ers,

        [Display("Seattle Seahawks")]
        [DisplayVariations("seattle", "hawks", "seahawks", "seattle hawks", "seattle seahawks", "sea")]
        SeattleSeahawks
    } 
    #endregion
}