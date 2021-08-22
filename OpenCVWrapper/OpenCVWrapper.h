#pragma once
#include <opencv2/opencv.hpp>
#include <msclr/marshal_cppstd.h>
using namespace System;
using namespace System::Windows;
using namespace System::Drawing;
using namespace msclr::interop;
namespace OpenCVWrapper {
	public ref class OpenCVWrapperCls
	{
	public:
		static Bitmap^ GetImageBitmap(String^ baseFilePath, String^ overlayFilePath)
		{
			auto base = cv::imread(marshal_as<std::string>(baseFilePath));
			auto overlay = cv::imread(marshal_as<std::string>(overlayFilePath));
			auto dst = base.clone();

			if (!overlay.empty()) {
				cv::resize(overlay, overlay, base.size());
				cv::addWeighted(base, 1.0, overlay, 0.4, 0.0, dst);
			}

			if (!dst.empty()) {
				Bitmap^ ret = gcnew Bitmap(dst.cols, dst.rows, System::Drawing::Imaging::PixelFormat::Format32bppArgb);
				for (int y = 0; y < dst.rows; y++) {
					for (int x = 0; x < dst.cols; x++) {
						auto a = 255;
						auto r = dst.data[(y * dst.step) + (x * 3) + 2];
						auto g = dst.data[(y * dst.step) + (x * 3) + 1];
						auto b = dst.data[(y * dst.step) + (x * 3) + 0];
						auto color = Color::FromArgb(a, r, g, b);
						ret->SetPixel(x, y, color);
					}
				}
				return ret;
			}

			return nullptr;
		}
	};
}