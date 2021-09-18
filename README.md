# grpc-example

Để sử dụng:
1. Docker hoặc không docker, hiện tại mới kiểm tra chạy với docker-compose
2. Để chạy được thì cần setup SSL để trust, cách thức tạo để trust:
	a. Dùng các file trong ssl-scripts để tạo file crt, key và pfx
	b. Chép crt và pfx vào thư mục %APPDATA%/ASP.NET/Https trên máy dev, do thư mục này được config để share với docker sẵn.
3. Gọi https://localhost/Weatherforecast dể test
