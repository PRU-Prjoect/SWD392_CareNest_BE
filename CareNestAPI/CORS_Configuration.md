# CORS Configuration for CareNest API

## Tổng quan
API đã được cấu hình để hỗ trợ CORS (Cross-Origin Resource Sharing) với các headers cần thiết cho tất cả responses.

## Cấu hình CORS

### 1. CORS Headers được thêm vào tất cả responses:
- `Access-Control-Allow-Origin: *` - Cho phép tất cả origins
- `Access-Control-Allow-Methods: GET, POST, PUT, DELETE, OPTIONS` - Cho phép các HTTP methods
- `Access-Control-Allow-Headers: Content-Type, Authorization` - Cho phép các headers cần thiết

### 2. Các lớp đã được tạo:

#### CorsMiddleware (Middleware/CorsMiddleware.cs)
- Thêm CORS headers vào tất cả responses
- Xử lý preflight OPTIONS requests
- Được đăng ký trong pipeline trước authentication

#### CorsActionFilter (Filters/CorsActionFilter.cs)
- Thêm CORS headers trước và sau khi thực thi action
- Đảm bảo headers được thêm vào cả success và error responses từ controllers

#### CorsExceptionFilter (Filters/CorsExceptionFilter.cs)
- Thêm CORS headers vào error responses khi có exception
- Tạo standardized error response format

### 3. Cấu hình trong Program.cs:
- Đăng ký CORS policy "AllowAll"
- Thêm custom middleware và filters
- Middleware được đặt trước authentication để xử lý OPTIONS requests

## Lưu ý bảo mật
- Cấu hình hiện tại cho phép tất cả origins (`*`) - phù hợp cho development
- Trong production, nên thay đổi `Access-Control-Allow-Origin` thành domain cụ thể
- Ví dụ: `Access-Control-Allow-Origin: https://yourdomain.com`

## Cách thay đổi cho production:
1. Thay đổi trong CorsMiddleware.cs:
```csharp
context.Response.Headers.Add("Access-Control-Allow-Origin", "https://yourdomain.com");
```

2. Thay đổi trong CorsActionFilter.cs:
```csharp
response.Headers.Add("Access-Control-Allow-Origin", "https://yourdomain.com");
```

3. Thay đổi trong CorsExceptionFilter.cs:
```csharp
response.Headers.Add("Access-Control-Allow-Origin", "https://yourdomain.com");
```

4. Cập nhật CORS policy trong Program.cs:
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("https://yourdomain.com")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
``` 