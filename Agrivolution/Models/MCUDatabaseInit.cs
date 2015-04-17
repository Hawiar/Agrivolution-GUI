using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Agrivolution.Models
{
    public class MCUDatabaseInit : DropCreateDatabaseIfModelChanges<MCUContext>
    {
        protected override void Seed(MCUContext context)
        {
            GetMCUs().ForEach(m => context.MCUs.Add(m));
        }

        private static List<MCU> GetMCUs()
        {
            var mcus = new List<MCU> {
                new MCU
                {
                    MCUID = 1,
                    Room = "Heatherfield",
                    Facility = "Churchill Building",
                    UserID = "001",
                    Type = "Pump",
                    Status = "Off",
                    Group = null,
                    Schedule = null,
                    MCUData = null,
                    MCUPhotos = null
                },

                new MCU
                {
                    MCUID = 2,
                    Room = "HeatherField",
                    Facility = "Churchill Building",
                    UserID = "001",
                    Type = "Tempature",
                    Status = "Off",
                    Group = null,
                    Schedule = null,
                    MCUData = null,
                    MCUPhotos = null
                },

                new MCU
                {
                    MCUID = 3,
                    Room = "Yellow",
                    Facility = "Churchill Building",
                    UserID = "001",
                    Type = "Tempature",
                    Status = "Off",
                    Group = null,
                    Schedule = null,
                    MCUData = null,
                    MCUPhotos = null
                },

                new MCU
                {
                    MCUID = 4,
                    Room = "Yellow",
                    Facility = "Churchill Building",
                    UserID = "001",
                    Type = "HVAC",
                    Status = "Off",
                    Group = null,
                    Schedule = null,
                    MCUData = null,
                    MCUPhotos = null
                },

                new MCU
                {
                    MCUID = 5,
                    Room = "Yellow",
                    Facility = "Churchill Building",
                    UserID = "009",
                    Type = "Tempature",
                    Status = "Off",
                    Group = null,
                    Schedule = null,
                    MCUData = null,
                    MCUPhotos = null
                },

                new MCU
                {
                    MCUID = 6,
                    Room = "HeatherField",
                    Facility = "Churchill Building",
                    UserID = "009",
                    Type = "Pump",
                    Status = "Off",
                    Group = null,
                    Schedule = null,
                    MCUData = null,
                    MCUPhotos = null
                },

                new MCU
                {
                    MCUID = 7,
                    Room = "HeatherField",
                    Facility = "Churchill Building",
                    UserID = "009",
                    Type = "Tempature",
                    Status = "Off",
                    Group = null,
                    Schedule = null,
                    MCUData = null,
                    MCUPhotos = null
                },

                new MCU
                {
                    MCUID = 8,
                    Room = "HeatherField",
                    Facility = "Churchill Building",
                    UserID = "001",
                    Type = "HVAC",
                    Status = "Off",
                    Group = null,
                    Schedule = null,
                    MCUData = null,
                    MCUPhotos = null
                },

                new MCU
                {
                    MCUID = 9,
                    Room = "Room 403",
                    Facility = "Oak Building",
                    UserID = "001",
                    Type = "Tempature",
                    Status = "Off",
                    Group = null,
                    Schedule = null,
                    MCUData = null,
                    MCUPhotos = null
                },

                new MCU
                {
                    MCUID = 10,
                    Room = "Room 403",
                    Facility = "Oak Building",
                    UserID = "009",
                    Type = "Pump",
                    Status = "Off",
                    Group = null,
                    Schedule = null,
                    MCUData = null,
                    MCUPhotos = null
                },

                new MCU
                {
                    MCUID = 11,
                    Room = "Room 403",
                    Facility = "Oak Building",
                    UserID = "009",
                    Type = "HVAC",
                    Status = "Off",
                    Group = null,
                    Schedule = null,
                    MCUData = null,
                    MCUPhotos = null
                },

                new MCU
                {
                    MCUID = 12,
                    Room = "Room 403",
                    Facility = "Oak Building",
                    UserID = "001",
                    Type = "Pump",
                    Status = "Off",
                    Group = null,
                    Schedule = null,
                    MCUData = null,
                    MCUPhotos = null
                },

                new MCU
                {
                    MCUID = 13,
                    Room = "Room 404",
                    Facility = "Oak Building",
                    UserID = "001",
                    Type = "Pump",
                    Status = "Off",
                    Group = null,
                    Schedule = null,
                    MCUData = null,
                    MCUPhotos = null
                },

                new MCU
                {
                    MCUID = 14,
                    Room = "Room 404",
                    Facility = "Churchill Building",
                    UserID = "001",
                    Type = "Tempature",
                    Status = "Off",
                    Group = null,
                    Schedule = null,
                    MCUData = null,
                    MCUPhotos = null
                },


            };
            return mcus;
        }
    }
}