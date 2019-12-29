using KaylaaShop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaylaaShop.Data
{
    public class SyncManagerRepo
    {
        private readonly KaylaaDataContext context;

        public SyncManagerRepo(KaylaaDataContext _context)
        {
            context = _context;
        }

        public SyncManager LogSyncEntry(SyncManager sync)
        {
           var syn= context.SyncManager.Add(sync);           
            return sync;
        }

        public SyncManager DeleteSyncEntry(SyncManager sync)
        {
            var syn = context.SyncManager.Remove(sync);
            context.SaveChanges();
            return sync;
        }

        public List<SyncManager> GetSyncEntryByShop(int shopid)
        {
            var syn = context.SyncManager.Where(c => c.ShopId == shopid).ToList();
            return syn;
        }

        public List<SyncManager> GetSyncEntryByDate(DateTime date)
        {
            var syn = context.SyncManager.Where(c => c.DateLogged.Date == date.Date).ToList();
            return syn;
        }

        public List<SyncManager> GetSyncEntryByEntity(string entityName)
        {
            var syn = context.SyncManager.Where(c => c.Entity == entityName).ToList();
            return syn;
        }

        public List<SyncManager> GetSyncEntryByTrackId(Guid trackId)
        {
            var syn = context.SyncManager.Where(c => c.SyncTrackId == trackId).ToList();
            return syn;
        }
    }
}
