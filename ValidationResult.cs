using System.Collections.Generic;

namespace Lab15_SoftwareQuality.Validation
{
    public class ValidationResult
    {
        public bool IsValid { get { return Errors.Count == 0; } }
        public List<string> Errors { get; } = new List<string>();
    }

    public class SinhVienDTO
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public float DiemTB { get; set; }
    }

    public class SinhVienValidation
    {
        public static ValidationResult Validate(SinhVienDTO dto)
        {
            ValidationResult result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(dto.MaSV))
                result.Errors.Add("Thiếu mã sinh viên.");

            if (string.IsNullOrWhiteSpace(dto.HoTen))
                result.Errors.Add("Thiếu họ tên.");

            if (dto.DiemTB < 0 || dto.DiemTB > 10)
                result.Errors.Add("Điểm không hợp lệ.");

            return result;
        }
    }
}