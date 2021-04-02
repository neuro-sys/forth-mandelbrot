# forth-mandelbrot

Mandelbrot set plotter in FORTH with fixed-point arithmetic.

Change fixed point precision in fp.fs to get higher quality.

Currently requires SDL 1.x via libtool. To make it work without SDL
and/or with a different Forth implementation, remove all SDL related
words, and implement put-pixel word yourself for your own platform.

<img src="https://user-images.githubusercontent.com/648622/113406051-7e295880-93b3-11eb-8236-fa018a587d5c.png" width="600">
<img src="https://user-images.githubusercontent.com/648622/113406060-808bb280-93b3-11eb-93fb-3614997b4899.png" width="600">
