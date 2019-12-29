
//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace SyncMan.Core

{
    public class staff  : IdentityUser
    {

     
        public string fullName { get; set; }
        public string address { get; set; }
        public string gender { get; set; }
        public string gaurantorName { get; set; }
        public string gaurantorPhoneNumber { get; set; }

        public int? shopId { get; set; }

        public int SyncId { get; set; }
        public Guid SyncTrackId { get; set; } //id for each entry trackable
        public int UnSyncedEvents { get; set; } //count of events unsynced in SyncManager Table
        public int SyncStatus { get; set; } //Out_Of_Sync, In_Sync
        public DateTime DateSynced { get; set; }


    }




}
