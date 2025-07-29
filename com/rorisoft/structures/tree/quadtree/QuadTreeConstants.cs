using System;
using System.Collections.Generic;
using System.Text;

namespace com.rorisoft.structures.tree.quadtree
{
    public class QuadTreeConstants
    {
        //public static double QUADTREE_LAST_NODE_SIZE_IN_KM = 100;
        public static double QUADTREE_LAST_NODE_SIZE_IN_KM = 100;
        //public static double QUADTREE_LAST_NODE_SIZE_IN_DEGREE = kmToDegree(QUADTREE_LAST_NODE_SIZE_IN_KM);
        public static float ONE_DEGREE_IN_KM = 111.0F;
        public static int TOTAL_X_DEGREES = 360; // -180 to 180 - longitude
        public static int TOTAL_Y_DEGREES = 180; // -90 to 90   - latitude
        public static int NORMALIZE_X = 180;
        public static int NORMALIZE_Y = 90;

        public static double kmToDegree(double km)
        {
            return km / ONE_DEGREE_IN_KM;
        }

        public static double QUADTREE_LAST_NODE_SIZE_IN_DEGREE()
        {
            return kmToDegree(QUADTREE_LAST_NODE_SIZE_IN_KM);
        }
    }
}
