namespace VaporStore.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using VaporStore.DataProcessor.Dto.Export;

    public static class Serializer
    {
        public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
        {
            throw new NotImplementedException();
        }

        public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
        {
            throw new NotImplementedException();
        }
    }
}