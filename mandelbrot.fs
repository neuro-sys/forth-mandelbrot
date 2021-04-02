\ Mandelbrot set plotter in FORTH with fixed-point arithmetic.
\
\ Change fixed point precision in fp.fs to get higher quality.
\
\ Currently requires SDL 1.x via libtool. To make it work without SDL
\ and/or with a different Forth implementation, remove all SDL related
\ words, and implement put-pixel word yourself for your own platform.

require sdl.fs
require fp.fs

also sdl.fs
also fp.fs

vocabulary mandelbrot.fs also mandelbrot.fs definitions

20  constant max-iter

3 i>fp 2 i>fp fp/             constant aspect-ratio

\ 0 271828 6 if2>fp negate  constant m-x0    \   0.2549870375144766
\ 0 314159 6 if2>fp negate  constant m-y0    \  -0.0005679790528465
\ 0 0001   4 if2>fp         constant m-width \   0.0000000000001

\ 0 16070135 8 if2>fp negate       constant m-x0    \ -0.16070135
\ 1 0375665  7 if2>fp              constant m-y0    \  1.0375665
\ 0 0000001  7 if2>fp              constant m-width \  0.0000001

\ 0 81153 5 if2>fp negate       constant m-x0    \ -0.81153
\ 0 20142 5 if2>fp              constant m-y0    \  0.20142
\ 0 00003 5 if2>fp              constant m-width \  0.00003

2 5 if>fp negate              constant m-x0  \ -2.5
1 0 if>fp negate              constant m-y0  \ -1.0
3 0 if>fp                     constant m-width \ 3.0

m-width aspect-ratio fp/      constant m-height

: co! ( addr -- )             dup rot swap cell + ! ! ;
: co@ ( addr -- )             dup @ swap cell + @ ;
: co+ ( F: x y u v -- x y )   rot + rot rot + swap ;
: co-abs^2 ( F: x0 y0 -- u )  dup fp* swap dup fp* + ;

variable x0
variable y0
variable x1
variable y1
: co* ( x0 y0 x1 y1 -- x2 y2 ) \ = (x0 + y0i)(x1 + y1i)
                               \ = (x0x1 - y0y1) + (x0y1 + y0x1)i

  y1 ! x1 ! y0 ! x0 !

  x0 @ x1 @ fp*
  y0 @ y1 @ fp* -
  x0 @ y1 @ fp*
  y0 @ x1 @ fp* +
;

: x-scale ( u1 -- fp ) #width  i>fp fp/ m-width  fp* m-x0 + ;
: y-scale ( u1 -- fp ) #height i>fp fp/ m-height fp* m-y0 + ;

2variable z
2variable c
variable in-set?
variable iteration-count
variable x
variable y

: colorize ( -- )
  iteration-count @ i>fp
  max-iter i>fp fp/
  255 i>fp fp* fp>i
  2 mod 255 *
  dup dup set-color
  x @ y @ put-pixel
;

: plot-mandel ( x y -- )
  y ! x !
  x @ i>fp x-scale
  y @ i>fp y-scale c co!
  0 0 z co!
  0 iteration-count !

  max-iter 0 do
    z co@ 2dup co*
    c co@ co+
    z co!           ( z = z^2 + c )
    iteration-count @ 1+ iteration-count !

    z co@ co-abs^2 4 i>fp > if
      colorize
      leave
    then
  loop
;

: render
  init-sdl
  #height 1- 0 do #width 1- 0 do i j plot-mandel loop flip-screen loop
  wait-key
  sdl-quit
;

render bye

previous definitions
