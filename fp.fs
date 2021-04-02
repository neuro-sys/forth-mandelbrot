[undefined] fp.fs [if]

vocabulary fp.fs also fp.fs definitions

\ adjust fractional point depending on your system's cell size
32                              constant point      \ fractional point
1 point lshift                  constant fp-bitmask
fp-bitmask 1-                   constant fp-fmask   \ fractional bit mask
fp-fmask invert                 constant fp-imask   \ integer bit mask
fp-bitmask 1 rshift             constant fp.bit/2   \ 0.5 in fp
fp-bitmask negate fp-bitmask *  constant max-fp     \ maximum value
fp-bitmask negate fp-bitmask *  constant min-fp     \ minimum value
10 point lshift                 constant fp10

: i>fp    ( n -- fp )           point lshift ;
: fp>i    ( fp -- n )           point rshift ;
: fp*     ( n0 n1 -- n2 )       fp-bitmask */ ;
: fp/     ( n0 n1 -- n2 )       fp-bitmask swap */ ;
: i3>fp3  ( a b c - a b c )     i>fp rot i>fp rot i>fp rot ;
: fp3>i3  ( a b c - a b c )     fp>i rot fp>i rot fp>i rot ;
: fpfloor ( n0 - n1 )           fp-imask and ;
: fpceil  ( n0 - n1 )           fp.bit/2 + fpfloor ;

\ return base 10 digits
: c10s    ( a -- b)             0 begin 1+ swap 10 / swap over 0> 0=
                                  until swap drop ;

\ convert a decimal fractional number in the form integer fractional
: if>fp   ( i f -- fp )
  dup i>fp swap
  c10s          ( i f 10s )
  0 do          ( i f )
    fp10 fp/
  loop
  swap i>fp +
;

\ supply the fractional digit count
: if2>fp   ( i f c -- fp )
  >r ( i f )
  i>fp
  r> 0 do        ( i f )
    fp10 fp/
  loop
  swap i>fp +
;

previous definitions

[then]
