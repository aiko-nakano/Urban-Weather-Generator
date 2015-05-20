using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTCI_calc
{
    public class ViewFactor
    {
        //using Fanger + Cannistraro paper
        double canHeight;
        double canWidth;
        double cog = 1.1; //center of gravity for a standing person   

        double sidewalk_width = 1.525 / 2; // middle of sidewalk
        double b_above;
        double b_below;

        public ViewFactor(double _canHeight, double _canWidth)
        {
            this.canHeight = _canHeight;
            this.canWidth = _canWidth;
            this.b_above = canHeight - cog;
            this.b_below = cog;
        }
        public double calc_viewFactor(double b, double c)
        {
            //constants for standing person from Tabls 3
            double Fmax = 0.119;
            double constant_C = 0.66134;
            double constant_D = 0.07363; 
            double gamma = constant_C + constant_D * (b / c);

            double F = 1 - Math.Exp(-1 * (b / c) / gamma);
            F *= Fmax;

            return F;
        }

        public double calc_F_wall(bool sidewalk)
        {
            double F_wall_above = 0.0;
            double F_wall_below = 0.0;

            if (sidewalk)
            {
                F_wall_above = 2 * calc_viewFactor(b_above, sidewalk_width);
                F_wall_above += 2 * calc_viewFactor(b_above, canWidth - sidewalk_width);

                F_wall_below = 2 * calc_viewFactor(b_below, sidewalk_width);
                F_wall_below += 2 * calc_viewFactor(b_below, canWidth - sidewalk_width);
            }
            else
            {
                F_wall_above = 4 * calc_viewFactor(b_above, canWidth / 2);
                F_wall_below = 4 * calc_viewFactor(b_below, canWidth / 2);
            }
            double F_wall = F_wall_above + F_wall_below;
            return F_wall;
        }

        public double calc_F_sky(bool sidewalk)
        {
            double F_sky = 0.0;

            if (sidewalk)
            {
                F_sky = 2 * calc_viewFactor(sidewalk_width, b_above);
                F_sky += 2 * calc_viewFactor(canWidth - sidewalk_width, b_above);
            }
            else
            {
                F_sky = 4 * calc_viewFactor(canWidth / 2, b_above);
            }
            return F_sky;
        }

        public double calc_F_road(bool sidewalk)
        {
            double F_road = 0.0;

            if (sidewalk)
            {
                F_road = 2 * calc_viewFactor(sidewalk_width, b_below);
                F_road += 2 * calc_viewFactor(canWidth - sidewalk_width, b_below);
            }
            else
            {
                F_road = 4 * calc_viewFactor(canWidth / 2, b_below);
            }
            return F_road;
        }
    }
}
