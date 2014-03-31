/*
 * Bones.cs - Mapping between Kinect SDK indexes and enumeration for better code readability
 * 			
 * 
 * 		Developer: Avraam Mavridis 12-Mar-2014
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumanBones
{
    public enum Bones
    {
        HipCenter = 0,
        Spine,
        ShoulderCenter,
        Head,
        ShoulderLeft,
        ElbowLeft,
        WristLeft,
        HandLeft,
        ShoulderRight,
        ElbowRight,
        WristRight,
        HandRight,
        HipLeft,
        KneeLeft,
        AnkleLeft,
        FootLeft,
        HipRight,
        KneeRight,
        AnkleRight,
        FootRight,
        PositionCount
    }

    public class BonesIndex
    {
        public static int getBoneIndex(Bones bone){
            return (int)bone;
        }
    }
}
