﻿using AutoMapper;
using BLL.Interfaces;
using BOL.DTOs;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class RatingService : IRatingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RatingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<List<RatingDTO>> GetAllAsync()
        {
            var ratings = await _unitOfWork._ratingRepo.GetAllAsync();
            return _mapper.Map<List<RatingDTO>>(ratings);
        }

        public async Task<RatingDTO> GetByIdAsync(Guid id)
        {
            var rating = await _unitOfWork._ratingRepo.GetByIdAsync(id);
            return _mapper.Map<RatingDTO>(rating);
        }

        public async Task<bool> CreateAsync(RatingDTO ratingDto)
        {
            var rating = _mapper.Map<Rating>(ratingDto);
            rating.id = Guid.NewGuid(); 
            await _unitOfWork._ratingRepo.AddAsync(rating);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> UpdateAsync(RatingDTO ratingDto)
        {
            var existingRating = await _unitOfWork._ratingRepo.GetByIdAsync(ratingDto.id)
                ?? throw new Exception();
            existingRating.updated_at = DateTime.UtcNow;
            existingRating.star = ratingDto.star;
            existingRating.customer_id = ratingDto.customer_id;
            existingRating.comment = ratingDto.comment;
            var rating = _mapper.Map<Rating>(ratingDto);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var rating = await _unitOfWork._ratingRepo.GetByIdAsync(id);
            if (rating == null) return false;

            await _unitOfWork._ratingRepo.RemoveAsync(rating);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }
    }
}
