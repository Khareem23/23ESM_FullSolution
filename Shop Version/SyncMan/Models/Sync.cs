using System;
using System.Collections.Generic;
using System.Text;

namespace SyncMan.Core
{

    public class Sync //to be inherited by any Entity that will require syncing
    {
        public int SyncId { get; set; }
        public Guid SyncTrackId { get; set; } //id for each entry trackable
        public int UnSyncedEvents { get; set; } //count of events unsynced in SyncManager Table
        public int SyncStatus { get; set; } //Out_Of_Sync, In_Sync
        public DateTime DateSynced { get; set; } 
    }

    public class SyncManager //stores sync data to be applied when 
    {
       
        public int Id { get; set; }

        public string SyncManagerId { get; set; }
        public string SyncTrackId { get; set; }
        public int ShopId { get; set; }
        public string SourceDataStore { get; set; }
        public string SourceDataStoreType { get; set; }
        public string DestinationDataStore { get; set; }
        public string DestinationDataStoreType { get; set; }
        public string Entity { get; set; } //Table
        public string State { get; set; } //json for the state
        public string Action { get; set; } //Insert, Update, Delete
        public int SyncExecutionStatus { get; set; } //this tells us if the state has been applied across board
        public DateTime DateLogged { get; set; }
        public DateTime DateApplied { get; set; }


    }

    public class SyncManagerX //stores sync data to be applied when 
    {
      
        public int Id { get; set; }
        
        public Guid SyncManagerId { get; set; }
        public Guid SyncTrackId { get; set; }
        public int ShopId { get; set; }
        public string SourceDataStore { get; set; }
        public string SourceDataStoreType { get; set; }
        public string DestinationDataStore { get; set; }
        public string DestinationDataStoreType { get; set; }        
        public string Entity { get; set; } //Table
        public string State { get; set; } //json for the state
        public string Action { get; set; } //Insert, Update, Delete
        public int SyncExecutionStatus { get; set; } //this tells us if the state has been applied across board
        public DateTime DateLogged { get; set; }
        public DateTime DateApplied { get; set; }


    }

}
