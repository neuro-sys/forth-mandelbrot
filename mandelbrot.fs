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

20 constant max-iter

: co! ( addr -- )             dup rot swap cell + ! ! ;
: co@ ( addr -- )             dup @ swap cell + @ ;
: co+ ( F: x y u v -- x y )   rot + rot rot + swap ;
: co-abs^2 ( F: x0 y0 -- u )  dup fp* swap dup fp* + ;

variable x
variable y
: complex^2 ( : x0 y0 -- x1 y1) \ x^2 - y^2 + 2xyi
  y ! x !

  x @ dup fp* \ x^2
  x @ y @ 2 i>fp fp* fp* \ 2xyi
  y @ dup fp* \ y^2
  rot swap - swap
;

: x-scale ( u1 -- fp ) #width  i>fp fp/ 3 5 if>fp fp* 2 5 if>fp negate + ;
: y-scale ( u1 -- fp ) #height i>fp fp/ 2 0 if>fp fp* 1 0 if>fp negate + ;

2variable z
2variable c
variable in-set?
variable counter
variable x
variable y
: plot-mandel ( x y -- )
  y ! x !
  x @ i>fp x-scale
  y @ i>fp y-scale c co!
  0 0 z co!
  0 counter !

  max-iter 0 do
    z co@ complex^2
    c co@ co+
    z co! \ z = z^2 + c
    counter @ 1+ counter !

    \ if |z| > 2
    z co@ co-abs^2 4 i>fp > if

      counter @ i>fp
      max-iter i>fp fp/
      255 i>fp fp* fp>i
      2 mod 255 *
      dup dup set-color
      x @ y @ put-pixel

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
