using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTCI_calc
{
    class utci
    {
        public double Ta;       //air temperature, degree Celsius
        public double ehPa;     // water vapour presure, hPa=hecto Pascal
        public double RH;     // RH calculated from ehPa
        public double Tmrt;     //mean radiant temperature, degree Celsius
        public double va;       //wind speed 10 m above ground level in m/s

        public utci(double Ta, double Tmrt, double RH, double va)
        {
            this.Ta = Ta;
            this.Tmrt = Tmrt;
            this.RH = RH;
            this.va = va;
        }

        public double calc_ehPa()
        {
            double[] g = new double[8] {
                    -2.8365744*Math.Pow(10,3),
				    -6.028076559*Math.Pow(10,3),
				    1.954263612*Math.Pow(10,1),
				    -2.737830188*Math.Pow(10,-2),
				    1.6261698*Math.Pow(10,-5),
				    7.0229056*Math.Pow(10,-10),
				    -1.8680009*Math.Pow(10,-13),
				    2.7150305
                };

            double Tk = Ta + 273.15; 		// air temp in K
            double es = g[7] * Math.Log(Tk);
            for (int i = 0; i < 7; i++)
            {
                es = es + g[i] * Math.Pow(Tk, i - 2);
            };

            es = Math.Exp(es) * 0.01;	// *0.01: convert Pa to hPa
            //double RH = ehPa * 100.0 / es;
            double ehPa = es * RH / 100.0;

            return ehPa;
        }

        public double calc_UTCI()
        {
            ehPa = this.calc_ehPa();
            double D_Tmrt = Tmrt - Ta;
            double Pa = ehPa / 10.0;    // use vapour pressure in kPa

            double UTCI_approx =
                Ta +
                (6.07562052) * Math.Pow(10, -01) +
                (-2.27712343) * Math.Pow(10, -02) * Ta +
                (8.06470249) * Math.Pow(10, -04) * Ta * Ta +
                (-1.54271372) * Math.Pow(10, -04) * Ta * Ta * Ta +
                (-3.24651735) * Math.Pow(10, -06) * Ta * Ta * Ta * Ta +
                (7.32602852) * Math.Pow(10, -08) * Ta * Ta * Ta * Ta * Ta +
                (1.35959073) * Math.Pow(10, -09) * Ta * Ta * Ta * Ta * Ta * Ta +
                (-2.25836520) * Math.Pow(10, +00) * va +
                (8.80326035) * Math.Pow(10, -02) * Ta * va +
                (2.16844454) * Math.Pow(10, -03) * Ta * Ta * va +
                (-1.53347087) * Math.Pow(10, -05) * Ta * Ta * Ta * va +
                (-5.72983704) * Math.Pow(10, -07) * Ta * Ta * Ta * Ta * va +
                (-2.55090145) * Math.Pow(10, -09) * Ta * Ta * Ta * Ta * Ta * va +
                (-7.51269505) * Math.Pow(10, -01) * va * va +
                (-4.08350271) * Math.Pow(10, -03) * Ta * va * va +
                (-5.21670675) * Math.Pow(10, -05) * Ta * Ta * va * va +
                (1.94544667) * Math.Pow(10, -06) * Ta * Ta * Ta * va * va +
                (1.14099531) * Math.Pow(10, -08) * Ta * Ta * Ta * Ta * va * va +
                (1.58137256) * Math.Pow(10, -01) * va * va * va +
                (-6.57263143) * Math.Pow(10, -05) * Ta * va * va * va +
                (2.22697524) * Math.Pow(10, -07) * Ta * Ta * va * va * va +
                (-4.16117031) * Math.Pow(10, -08) * Ta * Ta * Ta * va * va * va +
                (-1.27762753) * Math.Pow(10, -02) * va * va * va * va +
                (9.66891875) * Math.Pow(10, -06) * Ta * va * va * va * va +
                (2.52785852) * Math.Pow(10, -09) * Ta * Ta * va * va * va * va +
                (4.56306672) * Math.Pow(10, -04) * va * va * va * va * va +
                (-1.74202546) * Math.Pow(10, -07) * Ta * va * va * va * va * va +
                (-5.91491269) * Math.Pow(10, -06) * va * va * va * va * va * va +
                (3.98374029) * Math.Pow(10, -01) * D_Tmrt +
                (1.83945314) * Math.Pow(10, -04) * Ta * D_Tmrt +
                (-1.73754510) * Math.Pow(10, -04) * Ta * Ta * D_Tmrt +
                (-7.60781159) * Math.Pow(10, -07) * Ta * Ta * Ta * D_Tmrt +
                (3.77830287) * Math.Pow(10, -08) * Ta * Ta * Ta * Ta * D_Tmrt +
                (5.43079673) * Math.Pow(10, -10) * Ta * Ta * Ta * Ta * Ta * D_Tmrt +
                (-2.00518269) * Math.Pow(10, -02) * va * D_Tmrt +
                (8.92859837) * Math.Pow(10, -04) * Ta * va * D_Tmrt +
                (3.45433048) * Math.Pow(10, -06) * Ta * Ta * va * D_Tmrt +
                (-3.77925774) * Math.Pow(10, -07) * Ta * Ta * Ta * va * D_Tmrt +
                (-1.69699377) * Math.Pow(10, -09) * Ta * Ta * Ta * Ta * va * D_Tmrt +
                (1.69992415) * Math.Pow(10, -04) * va * va * D_Tmrt +
                (-4.99204314) * Math.Pow(10, -05) * Ta * va * va * D_Tmrt +
                (2.47417178) * Math.Pow(10, -07) * Ta * Ta * va * va * D_Tmrt +
                (1.07596466) * Math.Pow(10, -08) * Ta * Ta * Ta * va * va * D_Tmrt +
                (8.49242932) * Math.Pow(10, -05) * va * va * va * D_Tmrt +
                (1.35191328) * Math.Pow(10, -06) * Ta * va * va * va * D_Tmrt +
                (-6.21531254) * Math.Pow(10, -09) * Ta * Ta * va * va * va * D_Tmrt +
                (-4.99410301) * Math.Pow(10, -06) * va * va * va * va * D_Tmrt +
                (-1.89489258) * Math.Pow(10, -08) * Ta * va * va * va * va * D_Tmrt +
                (8.15300114) * Math.Pow(10, -08) * va * va * va * va * va * D_Tmrt +
                (7.55043090) * Math.Pow(10, -04) * D_Tmrt * D_Tmrt +
                (-5.65095215) * Math.Pow(10, -05) * Ta * D_Tmrt * D_Tmrt +
                (-4.52166564) * Math.Pow(10, -07) * Ta * Ta * D_Tmrt * D_Tmrt +
                (2.46688878) * Math.Pow(10, -08) * Ta * Ta * Ta * D_Tmrt * D_Tmrt +
                (2.42674348) * Math.Pow(10, -10) * Ta * Ta * Ta * Ta * D_Tmrt * D_Tmrt +
                (1.54547250) * Math.Pow(10, -04) * va * D_Tmrt * D_Tmrt +
                (5.24110970) * Math.Pow(10, -06) * Ta * va * D_Tmrt * D_Tmrt +
                (-8.75874982) * Math.Pow(10, -08) * Ta * Ta * va * D_Tmrt * D_Tmrt +
                (-1.50743064) * Math.Pow(10, -09) * Ta * Ta * Ta * va * D_Tmrt * D_Tmrt +
                (-1.56236307) * Math.Pow(10, -05) * va * va * D_Tmrt * D_Tmrt +
                (-1.33895614) * Math.Pow(10, -07) * Ta * va * va * D_Tmrt * D_Tmrt +
                (2.49709824) * Math.Pow(10, -09) * Ta * Ta * va * va * D_Tmrt * D_Tmrt +
                (6.51711721) * Math.Pow(10, -07) * va * va * va * D_Tmrt * D_Tmrt +
                (1.94960053) * Math.Pow(10, -09) * Ta * va * va * va * D_Tmrt * D_Tmrt +
                (-1.00361113) * Math.Pow(10, -08) * va * va * va * va * D_Tmrt * D_Tmrt +
                (-1.21206673) * Math.Pow(10, -05) * D_Tmrt * D_Tmrt * D_Tmrt +
                (-2.18203660) * Math.Pow(10, -07) * Ta * D_Tmrt * D_Tmrt * D_Tmrt +
                (7.51269482) * Math.Pow(10, -09) * Ta * Ta * D_Tmrt * D_Tmrt * D_Tmrt +
                (9.79063848) * Math.Pow(10, -11) * Ta * Ta * Ta * D_Tmrt * D_Tmrt * D_Tmrt +
                (1.25006734) * Math.Pow(10, -06) * va * D_Tmrt * D_Tmrt * D_Tmrt +
                (-1.81584736) * Math.Pow(10, -09) * Ta * va * D_Tmrt * D_Tmrt * D_Tmrt +
                (-3.52197671) * Math.Pow(10, -10) * Ta * Ta * va * D_Tmrt * D_Tmrt * D_Tmrt +
                (-3.36514630) * Math.Pow(10, -08) * va * va * D_Tmrt * D_Tmrt * D_Tmrt +
                (1.35908359) * Math.Pow(10, -10) * Ta * va * va * D_Tmrt * D_Tmrt * D_Tmrt +
                (4.17032620) * Math.Pow(10, -10) * va * va * va * D_Tmrt * D_Tmrt * D_Tmrt +
                (-1.30369025) * Math.Pow(10, -09) * D_Tmrt * D_Tmrt * D_Tmrt * D_Tmrt +
                (4.13908461) * Math.Pow(10, -10) * Ta * D_Tmrt * D_Tmrt * D_Tmrt * D_Tmrt +
                (9.22652254) * Math.Pow(10, -12) * Ta * Ta * D_Tmrt * D_Tmrt * D_Tmrt * D_Tmrt +
                (-5.08220384) * Math.Pow(10, -09) * va * D_Tmrt * D_Tmrt * D_Tmrt * D_Tmrt +
                (-2.24730961) * Math.Pow(10, -11) * Ta * va * D_Tmrt * D_Tmrt * D_Tmrt * D_Tmrt +
                (1.17139133) * Math.Pow(10, -10) * va * va * D_Tmrt * D_Tmrt * D_Tmrt * D_Tmrt +
                (6.62154879) * Math.Pow(10, -10) * D_Tmrt * D_Tmrt * D_Tmrt * D_Tmrt * D_Tmrt +
                (4.03863260) * Math.Pow(10, -13) * Ta * D_Tmrt * D_Tmrt * D_Tmrt * D_Tmrt * D_Tmrt +
                (1.95087203) * Math.Pow(10, -12) * va * D_Tmrt * D_Tmrt * D_Tmrt * D_Tmrt * D_Tmrt +
                (-4.73602469) * Math.Pow(10, -12) * D_Tmrt * D_Tmrt * D_Tmrt * D_Tmrt * D_Tmrt * D_Tmrt +
                (5.12733497) * Math.Pow(10, +00) * Pa +
                (-3.12788561) * Math.Pow(10, -01) * Ta * Pa +
                (-1.96701861) * Math.Pow(10, -02) * Ta * Ta * Pa +
                (9.99690870) * Math.Pow(10, -04) * Ta * Ta * Ta * Pa +
                (9.51738512) * Math.Pow(10, -06) * Ta * Ta * Ta * Ta * Pa +
                (-4.66426341) * Math.Pow(10, -07) * Ta * Ta * Ta * Ta * Ta * Pa +
                (5.48050612) * Math.Pow(10, -01) * va * Pa +
                (-3.30552823) * Math.Pow(10, -03) * Ta * va * Pa +
                (-1.64119440) * Math.Pow(10, -03) * Ta * Ta * va * Pa +
                (-5.16670694) * Math.Pow(10, -06) * Ta * Ta * Ta * va * Pa +
                (9.52692432) * Math.Pow(10, -07) * Ta * Ta * Ta * Ta * va * Pa +
                (-4.29223622) * Math.Pow(10, -02) * va * va * Pa +
                (5.00845667) * Math.Pow(10, -03) * Ta * va * va * Pa +
                (1.00601257) * Math.Pow(10, -06) * Ta * Ta * va * va * Pa +
                (-1.81748644) * Math.Pow(10, -06) * Ta * Ta * Ta * va * va * Pa +
                (-1.25813502) * Math.Pow(10, -03) * va * va * va * Pa +
                (-1.79330391) * Math.Pow(10, -04) * Ta * va * va * va * Pa +
                (2.34994441) * Math.Pow(10, -06) * Ta * Ta * va * va * va * Pa +
                (1.29735808) * Math.Pow(10, -04) * va * va * va * va * Pa +
                (1.29064870) * Math.Pow(10, -06) * Ta * va * va * va * va * Pa +
                (-2.28558686) * Math.Pow(10, -06) * va * va * va * va * va * Pa +
                (-3.69476348) * Math.Pow(10, -02) * D_Tmrt * Pa +
                (1.62325322) * Math.Pow(10, -03) * Ta * D_Tmrt * Pa +
                (-3.14279680) * Math.Pow(10, -05) * Ta * Ta * D_Tmrt * Pa +
                (2.59835559) * Math.Pow(10, -06) * Ta * Ta * Ta * D_Tmrt * Pa +
                (-4.77136523) * Math.Pow(10, -08) * Ta * Ta * Ta * Ta * D_Tmrt * Pa +
                (8.64203390) * Math.Pow(10, -03) * va * D_Tmrt * Pa +
                (-6.87405181) * Math.Pow(10, -04) * Ta * va * D_Tmrt * Pa +
                (-9.13863872) * Math.Pow(10, -06) * Ta * Ta * va * D_Tmrt * Pa +
                (5.15916806) * Math.Pow(10, -07) * Ta * Ta * Ta * va * D_Tmrt * Pa +
                (-3.59217476) * Math.Pow(10, -05) * va * va * D_Tmrt * Pa +
                (3.28696511) * Math.Pow(10, -05) * Ta * va * va * D_Tmrt * Pa +
                (-7.10542454) * Math.Pow(10, -07) * Ta * Ta * va * va * D_Tmrt * Pa +
                (-1.24382300) * Math.Pow(10, -05) * va * va * va * D_Tmrt * Pa +
                (-7.38584400) * Math.Pow(10, -09) * Ta * va * va * va * D_Tmrt * Pa +
                (2.20609296) * Math.Pow(10, -07) * va * va * va * va * D_Tmrt * Pa +
                (-7.32469180) * Math.Pow(10, -04) * D_Tmrt * D_Tmrt * Pa +
                (-1.87381964) * Math.Pow(10, -05) * Ta * D_Tmrt * D_Tmrt * Pa +
                (4.80925239) * Math.Pow(10, -06) * Ta * Ta * D_Tmrt * D_Tmrt * Pa +
                (-8.75492040) * Math.Pow(10, -08) * Ta * Ta * Ta * D_Tmrt * D_Tmrt * Pa +
                (2.77862930) * Math.Pow(10, -05) * va * D_Tmrt * D_Tmrt * Pa +
                (-5.06004592) * Math.Pow(10, -06) * Ta * va * D_Tmrt * D_Tmrt * Pa +
                (1.14325367) * Math.Pow(10, -07) * Ta * Ta * va * D_Tmrt * D_Tmrt * Pa +
                (2.53016723) * Math.Pow(10, -06) * va * va * D_Tmrt * D_Tmrt * Pa +
                (-1.72857035) * Math.Pow(10, -08) * Ta * va * va * D_Tmrt * D_Tmrt * Pa +
                (-3.95079398) * Math.Pow(10, -08) * va * va * va * D_Tmrt * D_Tmrt * Pa +
                (-3.59413173) * Math.Pow(10, -07) * D_Tmrt * D_Tmrt * D_Tmrt * Pa +
                (7.04388046) * Math.Pow(10, -07) * Ta * D_Tmrt * D_Tmrt * D_Tmrt * Pa +
                (-1.89309167) * Math.Pow(10, -08) * Ta * Ta * D_Tmrt * D_Tmrt * D_Tmrt * Pa +
                (-4.79768731) * Math.Pow(10, -07) * va * D_Tmrt * D_Tmrt * D_Tmrt * Pa +
                (7.96079978) * Math.Pow(10, -09) * Ta * va * D_Tmrt * D_Tmrt * D_Tmrt * Pa +
                (1.62897058) * Math.Pow(10, -09) * va * va * D_Tmrt * D_Tmrt * D_Tmrt * Pa +
                (3.94367674) * Math.Pow(10, -08) * D_Tmrt * D_Tmrt * D_Tmrt * D_Tmrt * Pa +
                (-1.18566247) * Math.Pow(10, -09) * Ta * D_Tmrt * D_Tmrt * D_Tmrt * D_Tmrt * Pa +
                (3.34678041) * Math.Pow(10, -10) * va * D_Tmrt * D_Tmrt * D_Tmrt * D_Tmrt * Pa +
                (-1.15606447) * Math.Pow(10, -10) * D_Tmrt * D_Tmrt * D_Tmrt * D_Tmrt * D_Tmrt * Pa +
                (-2.80626406) * Math.Pow(10, +00) * Pa * Pa +
                (5.48712484) * Math.Pow(10, -01) * Ta * Pa * Pa +
                (-3.99428410) * Math.Pow(10, -03) * Ta * Ta * Pa * Pa +
                (-9.54009191) * Math.Pow(10, -04) * Ta * Ta * Ta * Pa * Pa +
                (1.93090978) * Math.Pow(10, -05) * Ta * Ta * Ta * Ta * Pa * Pa +
                (-3.08806365) * Math.Pow(10, -01) * va * Pa * Pa +
                (1.16952364) * Math.Pow(10, -02) * Ta * va * Pa * Pa +
                (4.95271903) * Math.Pow(10, -04) * Ta * Ta * va * Pa * Pa +
                (-1.90710882) * Math.Pow(10, -05) * Ta * Ta * Ta * va * Pa * Pa +
                (2.10787756) * Math.Pow(10, -03) * va * va * Pa * Pa +
                (-6.98445738) * Math.Pow(10, -04) * Ta * va * va * Pa * Pa +
                (2.30109073) * Math.Pow(10, -05) * Ta * Ta * va * va * Pa * Pa +
                (4.17856590) * Math.Pow(10, -04) * va * va * va * Pa * Pa +
                (-1.27043871) * Math.Pow(10, -05) * Ta * va * va * va * Pa * Pa +
                (-3.04620472) * Math.Pow(10, -06) * va * va * va * va * Pa * Pa +
                (5.14507424) * Math.Pow(10, -02) * D_Tmrt * Pa * Pa +
                (-4.32510997) * Math.Pow(10, -03) * Ta * D_Tmrt * Pa * Pa +
                (8.99281156) * Math.Pow(10, -05) * Ta * Ta * D_Tmrt * Pa * Pa +
                (-7.14663943) * Math.Pow(10, -07) * Ta * Ta * Ta * D_Tmrt * Pa * Pa +
                (-2.66016305) * Math.Pow(10, -04) * va * D_Tmrt * Pa * Pa +
                (2.63789586) * Math.Pow(10, -04) * Ta * va * D_Tmrt * Pa * Pa +
                (-7.01199003) * Math.Pow(10, -06) * Ta * Ta * va * D_Tmrt * Pa * Pa +
                (-1.06823306) * Math.Pow(10, -04) * va * va * D_Tmrt * Pa * Pa +
                (3.61341136) * Math.Pow(10, -06) * Ta * va * va * D_Tmrt * Pa * Pa +
                (2.29748967) * Math.Pow(10, -07) * va * va * va * D_Tmrt * Pa * Pa +
                (3.04788893) * Math.Pow(10, -04) * D_Tmrt * D_Tmrt * Pa * Pa +
                (-6.42070836) * Math.Pow(10, -05) * Ta * D_Tmrt * D_Tmrt * Pa * Pa +
                (1.16257971) * Math.Pow(10, -06) * Ta * Ta * D_Tmrt * D_Tmrt * Pa * Pa +
                (7.68023384) * Math.Pow(10, -06) * va * D_Tmrt * D_Tmrt * Pa * Pa +
                (-5.47446896) * Math.Pow(10, -07) * Ta * va * D_Tmrt * D_Tmrt * Pa * Pa +
                (-3.59937910) * Math.Pow(10, -08) * va * va * D_Tmrt * D_Tmrt * Pa * Pa +
                (-4.36497725) * Math.Pow(10, -06) * D_Tmrt * D_Tmrt * D_Tmrt * Pa * Pa +
                (1.68737969) * Math.Pow(10, -07) * Ta * D_Tmrt * D_Tmrt * D_Tmrt * Pa * Pa +
                (2.67489271) * Math.Pow(10, -08) * va * D_Tmrt * D_Tmrt * D_Tmrt * Pa * Pa +
                (3.23926897) * Math.Pow(10, -09) * D_Tmrt * D_Tmrt * D_Tmrt * D_Tmrt * Pa * Pa +
                (-3.53874123) * Math.Pow(10, -02) * Pa * Pa * Pa +
                (-2.21201190) * Math.Pow(10, -01) * Ta * Pa * Pa * Pa +
                (1.55126038) * Math.Pow(10, -02) * Ta * Ta * Pa * Pa * Pa +
                (-2.63917279) * Math.Pow(10, -04) * Ta * Ta * Ta * Pa * Pa * Pa +
                (4.53433455) * Math.Pow(10, -02) * va * Pa * Pa * Pa +
                (-4.32943862) * Math.Pow(10, -03) * Ta * va * Pa * Pa * Pa +
                (1.45389826) * Math.Pow(10, -04) * Ta * Ta * va * Pa * Pa * Pa +
                (2.17508610) * Math.Pow(10, -04) * va * va * Pa * Pa * Pa +
                (-6.66724702) * Math.Pow(10, -05) * Ta * va * va * Pa * Pa * Pa +
                (3.33217140) * Math.Pow(10, -05) * va * va * va * Pa * Pa * Pa +
                (-2.26921615) * Math.Pow(10, -03) * D_Tmrt * Pa * Pa * Pa +
                (3.80261982) * Math.Pow(10, -04) * Ta * D_Tmrt * Pa * Pa * Pa +
                (-5.45314314) * Math.Pow(10, -09) * Ta * Ta * D_Tmrt * Pa * Pa * Pa +
                (-7.96355448) * Math.Pow(10, -04) * va * D_Tmrt * Pa * Pa * Pa +
                (2.53458034) * Math.Pow(10, -05) * Ta * va * D_Tmrt * Pa * Pa * Pa +
                (-6.31223658) * Math.Pow(10, -06) * va * va * D_Tmrt * Pa * Pa * Pa +
                (3.02122035) * Math.Pow(10, -04) * D_Tmrt * D_Tmrt * Pa * Pa * Pa +
                (-4.77403547) * Math.Pow(10, -06) * Ta * D_Tmrt * D_Tmrt * Pa * Pa * Pa +
                (1.73825715) * Math.Pow(10, -06) * va * D_Tmrt * D_Tmrt * Pa * Pa * Pa +
                (-4.09087898) * Math.Pow(10, -07) * D_Tmrt * D_Tmrt * D_Tmrt * Pa * Pa * Pa +
                (6.14155345) * Math.Pow(10, -01) * Pa * Pa * Pa * Pa +
                (-6.16755931) * Math.Pow(10, -02) * Ta * Pa * Pa * Pa * Pa +
                (1.33374846) * Math.Pow(10, -03) * Ta * Ta * Pa * Pa * Pa * Pa +
                (3.55375387) * Math.Pow(10, -03) * va * Pa * Pa * Pa * Pa +
                (-5.13027851) * Math.Pow(10, -04) * Ta * va * Pa * Pa * Pa * Pa +
                (1.02449757) * Math.Pow(10, -04) * va * va * Pa * Pa * Pa * Pa +
                (-1.48526421) * Math.Pow(10, -03) * D_Tmrt * Pa * Pa * Pa * Pa +
                (-4.11469183) * Math.Pow(10, -05) * Ta * D_Tmrt * Pa * Pa * Pa * Pa +
                (-6.80434415) * Math.Pow(10, -06) * va * D_Tmrt * Pa * Pa * Pa * Pa +
                (-9.77675906) * Math.Pow(10, -06) * D_Tmrt * D_Tmrt * Pa * Pa * Pa * Pa +
                (8.82773108) * Math.Pow(10, -02) * Pa * Pa * Pa * Pa * Pa +
                (-3.01859306) * Math.Pow(10, -03) * Ta * Pa * Pa * Pa * Pa * Pa +
                (1.04452989) * Math.Pow(10, -03) * va * Pa * Pa * Pa * Pa * Pa +
                (2.47090539) * Math.Pow(10, -04) * D_Tmrt * Pa * Pa * Pa * Pa * Pa +
                (1.48348065) * Math.Pow(10, -03) * Pa * Pa * Pa * Pa * Pa * Pa;

            return UTCI_approx;
        }
    }
}
