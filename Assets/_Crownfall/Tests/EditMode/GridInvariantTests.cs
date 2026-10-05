#if UNITY_INCLUDE_TESTS
using Crownfall.Battle.Grid; using Crownfall.Core; using Crownfall.Units; using NUnit.Framework; using UnityEngine;
namespace Crownfall.Tests.EditMode {
public sealed class GridInvariantTests {
 Unit MakeUnit(string name){var go=new GameObject(name);var def=ScriptableObject.CreateInstance<UnitDefinition>();def.unitId=name;def.displayName=name;def.maxHealth=100;var u=go.AddComponent<Unit>();u.Initialize(def,TeamId.Player);return u;}
 [Test] public void Cell_AllowsOnlyOneOccupant(){var c=new GridCell(new GridCoordinate(0,0),Vector3.zero);var a=MakeUnit("a");var b=MakeUnit("b");Assert.True(c.TryOccupy(a));Assert.False(c.TryOccupy(b));Object.DestroyImmediate(a.gameObject);Object.DestroyImmediate(b.gameObject);}
 [Test] public void Reservation_IsExclusive(){var c=new GridCell(new GridCoordinate(0,0),Vector3.zero);var a=MakeUnit("a");var b=MakeUnit("b");Assert.True(c.TryReserve(a));Assert.False(c.TryReserve(b));c.ClearReservation(a);Assert.True(c.TryReserve(b));Object.DestroyImmediate(a.gameObject);Object.DestroyImmediate(b.gameObject);}
}}
#endif
