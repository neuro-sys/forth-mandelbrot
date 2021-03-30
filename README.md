# forth-mandelbrot

Mandelbrot set plotter in FORTH with fixed-point arithmetic.

Change fixed point precision in fp.fs to get higher quality.

Currently requires SDL 1.x via libtool. To make it work without SDL
and/or with a different Forth implementation, remove all SDL related
words, and implement put-pixel word yourself for your own platform.


