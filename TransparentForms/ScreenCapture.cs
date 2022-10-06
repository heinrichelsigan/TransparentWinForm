using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace TransparentForms
{
    public class ScreenCapture
    {
        /// <summary>
        /// Creates an Image object containing a screen shot of the entire desktop
        /// </summary>
        /// <returns></returns>
        public Image CaptureScreen()
        {
            return CaptureWindow(User32.GetDesktopWindow());
        }

        /// <summary>
        /// Creates an Image object containing a screen shot of the entire desktop and child windows
        /// </summary>
        /// <returns>Array of Images</returns>
        public Image[] CaptureAllWindows()
        {
            List<Image> windowImages = new List<Image>();
            IntPtr deskPtr = User32.GetDesktopWindow();
            Image imageDesk = CaptureWindow(deskPtr);
            windowImages.Add(imageDesk);
            IntPtr topPtr = User32.GetTopWindow(deskPtr);
            Image imageTop = CaptureWindow(topPtr);
            windowImages.Add(imageTop);
            IntPtr nextPtr = topPtr;
            for (int i = 0; i < 16384; i++)
            {
                try
                {
                    nextPtr = User32.GetWindow(nextPtr, User32.GW_HWNDNEXT);
                    Image nextImage = CaptureWindow(nextPtr);
                    if (nextImage.Height > 1 && nextImage.Width > 1)
                        windowImages.Add(nextImage);
                }
                catch (Exception) { }
            }

            return windowImages.ToArray();
        }

        /// <summary>
        /// Creates an Image object containing a screen shot of a specific window
        /// </summary>
        /// <param name="handle">The handle to the window. (In windows forms, this is obtained by the Handle property)</param>
        /// <returns>Image</returns>
        public Image CaptureWindow(IntPtr handle)
        {
            // get te hDC of the target window
            IntPtr hdcSrc = User32.GetWindowDC(handle);
            // get the size
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(handle, ref windowRect);
            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;
            // create a device context we can copy to
            IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
            // create a bitmap we can copy it to,
            // using GetDeviceCaps to get the width/height
            IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
            // select the bitmap object
            IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
            // bitblt over
            GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.SRCCOPY);
            // restore selection
            GDI32.SelectObject(hdcDest, hOld);
            // clean up
            GDI32.DeleteDC(hdcDest);
            User32.ReleaseDC(handle, hdcSrc);
            // get a .NET image object for it
            Image img = Image.FromHbitmap(hBitmap);
            // free up the Bitmap object
            GDI32.DeleteObject(hBitmap);
            return img;
        }

        /// <summary>
        /// Captures a screen shot of a specific window, and saves it to a file
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        public void CaptureWindowToFile(IntPtr handle, string filename, ImageFormat format)
        {
            Image img = CaptureWindow(handle);
            img.Save(filename, format);
        }

        /// <summary>
        /// Captures a screen shot of the entire desktop, and saves it to a file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        public void CaptureScreenToFile(string filename, ImageFormat format)
        {
            Image img = CaptureScreen();
            img.Save(filename, format);
        }


        /// <summary>
        /// Captures a screen shot of the entire desktop and all child windows and saves it tó a directory
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="format"></param>
        public void CaptureScreenAndAllWindowsToDirectory(string directory, ImageFormat format)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            Image[] imgs = CaptureAllWindows();
            int ix = 0;
            foreach (Image img in imgs)
            {
                string filename = Path.Combine(directory, DateTime.Now.Ticks.ToString() + ix++ + ".png");
                img.Save(filename, format);
            }

            string[] files = Directory.GetFiles(directory);
            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);
                if (fi.Length <= 2048)
                    fi.Delete();
            }
        }

        /// <summary>
        /// Helper class containing Gdi32 API functions
        /// </summary>
        private class GDI32
        {
            public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter            

            [DllImport("gdi32.dll")]
            public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
                int nWidth, int nHeight, IntPtr hObjectSource,
                int nXSrc, int nYSrc, int dwRop);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth,
                int nHeight);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr hObject);
            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        }

        /// <summary>
        /// Helper class containing User32 API functions
        /// </summary>
        private class User32
        {
            public const uint GW_HWNDFIRST = 0x000;
            public const uint GW_HWNDLAST = 0x001;
            public const uint GW_HWNDNEXT = 0x002;
            public const uint GW_HWNDPREV = 0x003;
            public const uint GW_OWNER = 0x004;
            public const uint GW_CHILD = 0x005;
            public const uint GW_ENABLEDPOPUP = 0x006;

            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }
            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);
            [DllImport("user32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
            [DllImport("user32.dll")]
            public static extern IntPtr GetTopWindow(IntPtr hWnd);
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);
        }

    }

}
