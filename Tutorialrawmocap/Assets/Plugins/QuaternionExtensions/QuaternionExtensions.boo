[Extension]
public def Log(ref a as Quaternion):
	a0 as single = a.w
	a.w = 0.0
	if(Mathf.Abs(a0) < 1.0):
		angle as single = Mathf.Acos(a0)
		sinAngle  as single = Mathf.Sin(angle)
		if(Mathf.Abs(sinAngle) >= 1.0e-15):
			coeff as single = angle/sinAngle
			a.x *= coeff
			a.y *= coeff
			a.z *= coeff

[Extension]
public def Conjugate(ref a as Quaternion):
	a.x *= -1
	a.y *= -1
	a.z *= -1

[Extension]
public def Conjugated(a as Quaternion):
	result as Quaternion = a
	result.x *= -1
	result.y *= -1
	result.z *= -1
	return result

[Extension]
public def Scale(ref a as Quaternion, s as single):
	a.w *= s
	a.x *= s
	a.y *= s
	a.z *= s

[Extension]
public def Exp(ref a as Quaternion):
	angle as single = Mathf.Sqrt(a.x*a.x + a.y*a.y + a.z*a.z)
	sinAngle  as single = Mathf.Sin(angle)
	a.w = Mathf.Cos(angle)
	if(Mathf.Abs(sinAngle) >= 1.0e-15):
		coeff as single = sinAngle/angle
		a.x *= coeff
		a.y *= coeff
		a.z *= coeff


[Extension]
public def Normalize(ref a as Quaternion):
	length as single = a.Length()
	if(length > 1.0e-15):
		invlen as single = 1.0 / length
		a.w *= invlen
		a.x *= invlen
		a.y *= invlen
		a.z *= invlen
	else:
		length = 0.0
		a.w = 0.0
		a.x = 0.0
		a.y = 0.0
		a.z = 0.0
	return length


[Extension]
public def Length(a as Quaternion):
	return Mathf.Sqrt(a.w*a.w + a.x*a.x + a.y*a.y + a.z*a.z) 

[Extension]
static def op_Addition(a as Quaternion, b as Quaternion):
	return QuaternionExtensions.Add(a, b)

[Extension]
static def op_Subtraction(a as Quaternion, b as Quaternion):
	return QuaternionExtensions.Sub(a, b)

static class QuaternionExtensions():
	def Add(a as Quaternion, b as Quaternion):
		r as Quaternion
		r.w = a.w+b.w
		r.x = a.x+b.x
		r.y = a.y+b.y
		r.z = a.z+b.z
		return r

	def Sub(a as Quaternion, b as Quaternion):
		r as Quaternion
		r.w = a.w-b.w
		r.x = a.x-b.x
		r.y = a.y-b.y
		r.z = a.z-b.z
		return r

	def SlerpNoInvert(fro as Quaternion, to as Quaternion, factor as single):
		dot as single = Quaternion.Dot(fro,to);

		if (Mathf.Abs(dot) > 0.9999f): 
			return fro;
		
		theta as single = Mathf.Acos(dot)
		sinT = 1.0f / Mathf.Sin(theta)
		newFactor = Mathf.Sin(factor * theta) * sinT
		invFactor = Mathf.Sin((1.0f - factor) * theta) * sinT;

		return Quaternion( invFactor * fro.x + newFactor * to.x,invFactor * fro.y + newFactor * to.y,invFactor * fro.z + newFactor * to.z, invFactor * fro.w + newFactor * to.w );
