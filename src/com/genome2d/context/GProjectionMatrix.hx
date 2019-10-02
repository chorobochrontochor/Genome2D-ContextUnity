/*
 * 	Genome2D - 2D GPU Framework
 * 	http://www.genome2d.com
 *
 *	Copyright 2011-2014 Peter Stefcek. All rights reserved.
 *
 *	License:: ./doc/LICENSE.md (https://github.com/pshtif/Genome2D/blob/master/LICENSE.md)
 */
package com.genome2d.context;
import com.genome2d.geom.GMatrix3D;

class GProjectionMatrix extends GMatrix3D
{
    static public var NEAR:Int = 0;
    static public var FAR:Int = 4000;
	static private var g2d_instance:GProjectionMatrix;

    public function new() {
		super();

        reset();
    }

    static public function getOrtho(p_width:Float, p_height:Float, p_transform:GMatrix3D):GProjectionMatrix {
        if (g2d_instance == null) g2d_instance = new GProjectionMatrix();
        return g2d_instance.ortho(p_width, p_height, p_transform);
    }
    
    public function reset():Void {
        rawData[0] = 2.0;
        rawData[1] = 0.0;
        rawData[2] = 0.0;
        rawData[3] = 0.0;

        rawData[4] = 0.0;
        rawData[5] = -2.0;
        rawData[6] = 0.0;
        rawData[7] = 0.0;

        rawData[8] = 0.0;
        rawData[9] = 0.0;
        rawData[10] = 1/(FAR-NEAR);
        rawData[11] = -NEAR/(FAR-NEAR);

        rawData[12] = -1.0;
        rawData[13] = 1.0;
        rawData[14] = 0.0;
        rawData[15] = 1.0;
    }

    public function ortho(p_width:Float, p_height:Float, p_transform:GMatrix3D):GProjectionMatrix {
        rawData[0] = 2 / p_width;
        rawData[5] = -2 / p_height;

        if (p_transform != null) this.prepend(p_transform);

        return this;
    }
	
	// Render to texture needs different ortho matrix?!?
	public function orthoRtt(p_width:Float, p_height:Float, p_transform:GMatrix3D):GProjectionMatrix {
        rawData[0] = 2 / p_width;
        rawData[5] = -2 / p_height;

        //nativeMatrix[12] = -1;
		//nativeMatrix[13] = -1;

        if (p_transform != null) this.prepend(p_transform);

        return this;
    }

    public function perspective(p_width:Float, p_height:Float, zNear:Float, zFar:Float):GProjectionMatrix {
        /*
        rawData =  new Float32Array([2/p_width , 0.0         , 0.0                       , 0.0,
                                     0.0       , -2/p_height , 0.0                       , 0.0,
                                     0         , 0           , zFar/(zFar-zNear)         , 1.0,
									 0         , 0           , (zNear*zFar)/(zNear-zFar) , 0
										]);
                                        */
        return this;
    }
}