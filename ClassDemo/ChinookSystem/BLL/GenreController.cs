using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Chinook.Data.Entities;
using ChinookSystem.DAL;
using System.ComponentModel; //expose methods for ODS Wizard
using Chinook.Data.POCOs;
using Chinook.Data.DTOs;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class GenreController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<GenreDTO> Genre_GenreAlbumTracks()
        {
            using (var context = new ChinookContext())
            {
                var results = from x in context.Genres //This is the DBSet in context
                              select new GenreDTO
                {
                    genre = x.Name,
                    albums = from y in x.Tracks
                             group y by y.Album into gresults //.Album is the key data attribute group
                             select new AlbumDTO
                             {
                                 title = gresults.Key.Title,
                                 releaseyear = gresults.Key.ReleaseYear,
                                 numberoftracks = gresults.Count(),
                                 tracks = from z in gresults
                                          select new TrackPOCO
                                          {
                                              song = z.Name,
                                              milliseconds = z.Milliseconds
                                          }
                             }
                };
                return results.ToList();
            }
        }
    }
}
