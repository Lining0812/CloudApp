using CloudEFCore;
using CloudWebApi.Models;

namespace CloudWebApi.Services
{
    public class SingleRepository
    {
        private readonly MyDBContext _context;
        public SingleRepository(MyDBContext context)
        {
            _context = context;
        }

        public SingleSongDto GetAllSinges()
        {
            var single = _context.SingleSongs.FirstOrDefault();
            if (single == null)
            {
                return new SingleSongDto
                {
                    ID = 0,
                    Name = "没语季节"
                };
            }
            return new SingleSongDto
            {
                ID = single.ID,
                Name = single.Name
            };

            //return new SingleSongDto
            //{
            //    ID = 0,
            //    Name = "没语季节"
            //};
        }
    }
}
