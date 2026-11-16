using Lab15_SoftwareQuality.Validation;

namespace Lab15_SoftwareQuality.BUS
{
    public class SinhVienBUS
    {
        public bool ThemMoi(SinhVienDTO dto, out string errorMessage)
        {
            // Gọi dịch vụ kiểm tra dùng chung
            ValidationResult validation = SinhVienValidation.Validate(dto);

            if (!validation.IsValid)
            {
                // Gom tất cả lỗi thành danh sách có dấu chấm tròn
                errorMessage = string.Join("\n• ", validation.Errors);
                errorMessage = "• " + errorMessage;
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }
    }
}