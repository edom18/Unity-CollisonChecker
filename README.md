# This is a collision checker in Unity

このサンプルプロジェクトは、update timeを使わずに直に衝突位置を検知する方法のサンプルです。
This sample project shows you how to detect collision point without update time.

メインスクリプトは `CollisionChecker.cs` と `CollisionCheckerBit.cs` です。
Main scripts are `CollisionChecker.cs` and `CollisionCheckerBit.cs`.

`CollisionChecker.cs` はコリジョンを予測させたいオブジェクトに付けます。
`CollisionChecker.cs` is attached to a object you want to predict the collision.

`CollisionCheckerBit.cs` は検知用に射出され、実際に物理オブジェクトに力が加わった場合にどこに衝突するかを検知するオブジェクトを生成します。
`CollisionCheckerBit.cs` will be shot for detecting the collision when it has been added a force.
