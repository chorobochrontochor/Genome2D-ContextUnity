/*
 * 	Genome2D - 2D GPU Framework
 * 	http://www.genome2d.com
 *
 *	Copyright 2011-2019 Peter Stefcek. All rights reserved.
 *
 *	License:: ./doc/LICENSE.md (https://github.com/pshtif/Genome2D/blob/master/LICENSE.md)
 */
package com.genome2d.geom;

class GPoint {
    public var x:Float;
    public var y:Float;

    public function new(p_x:Float, p_y:Float) {
        x = p_x;
        y = p_y;
    }

    static public function distance(point1:GPoint, point2:GPoint):Float {

        var distX:Float = point2.x - point1.x;
        distX *= distX;
        var distY:Float = point2.y - point1.y;
        distY *= distY;

        return Math.sqrt(distX + distY);
    }

    public var length(get, never):Float;
    inline private function get_length():Float {
        return Math.sqrt(x*x + y*y);
    }  
}