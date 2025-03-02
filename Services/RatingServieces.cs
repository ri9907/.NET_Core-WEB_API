using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RatingServieces :IRatingServieces
    {
        private IRatingRepository _ratingRepository;

        public RatingServieces(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task<int> Add(Rating rating)
        {
            return await _ratingRepository.Add(rating);
        }

    }
}
