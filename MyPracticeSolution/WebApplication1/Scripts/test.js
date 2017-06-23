var result1;
if ("prerender" == M.visibilityState) {
    result1 = !1;//false
} else {
    //Z.D(["provide", "render", ua]);
    //var test1 = Z.J.apply(Z, [["provide", "render", ua]])  
    
    
    var sc = function (a) {
        if (ea(a[0]))
            this.u = a[0];
        else {
            var b = td.exec(a[0]);
            null != b && 4 == b.length && (this.c = b[1] || "t0",
            this.K = b[2] || "",
            this.C = b[3],
            this.a = [].slice.call(a, 1),
            this.K || (this.A = "create" == this.C,
            this.i = "require" == this.C,
            this.g = "provide" == this.C,
            this.ba = "remove" == this.C),
            this.i && (3 <= this.a.length ? (this.X = this.a[1],
            this.W = this.a[2]) : this.a[1] && (qa(this.a[1]) ? this.X = this.a[1] : this.W = this.a[1])));
            b = a[1];
            a = a[2];
            if (!this.C)
                throw "abort";
            if (this.i && (!qa(b) || "" == b))
                throw "abort";
            if (this.g && (!qa(b) || "" == b || !ea(a)))
                throw "abort";
            if (ud(this.c) || ud(this.K))
                throw "abort";
            if (this.g && "t0" != this.c)
                throw "abort";
        }
    };

    var ua = function () { }
    var args1 = ["provide", "render", ua];
    for (var b = [], c = 0; c < args1.length; c++)
        try {
            var d = new sc(args1[c]);
            d.g ? C(d.a[0], d.a[1]) : (d.i && (d.ha = y(d.c, d.a[0], d.X, d.W)), b.push(d))
        } catch (e) {}
    var test1 = b;


        test1 = Z.f.concat(test1);
        for (Z.f = []; 0 < test1.length && !Z.v(test1[0]) && !(test1.shift(),
    0 < Z.f.length) ;)
        ;
        Z.f = Z.f.concat(test1)



    result1 = !0;//true
}
    