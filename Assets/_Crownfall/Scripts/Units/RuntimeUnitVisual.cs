using Crownfall.Core;
using UnityEngine;

namespace Crownfall.Units
{
    /// <summary>Lightweight runtime presentation for source-only builds that have no authored unit prefabs yet.</summary>
    public sealed class RuntimeUnitVisual : MonoBehaviour
    {
        private Unit _unit;
        private Transform _barFill;
        private static Sprite _pixel;

        public void Bind(Unit unit)
        {
            _unit = unit;
            if (_pixel == null)
            {
                var tex = new Texture2D(1, 1, TextureFormat.RGBA32, false);
                tex.name = "CrownfallRuntimePixel";
                tex.SetPixel(0, 0, Color.white);
                tex.Apply();
                _pixel = Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(.5f, .5f), 1f);
            }

            var body = new GameObject("Body", typeof(SpriteRenderer));
            body.transform.SetParent(transform, false);
            body.transform.localScale = new Vector3(.72f, .72f, 1f);
            var sr = body.GetComponent<SpriteRenderer>();
            sr.sprite = _pixel;
            sr.sortingOrder = 20;
            sr.color = unit.Team == TeamId.Player
                ? new Color(.35f, .72f, .92f, 1f)
                : new Color(.90f, .34f, .28f, 1f);

            var shadow = new GameObject("Shadow", typeof(SpriteRenderer));
            shadow.transform.SetParent(transform, false);
            shadow.transform.localPosition = new Vector3(0f, -.38f, .1f);
            shadow.transform.localScale = new Vector3(.75f, .18f, 1f);
            var ss = shadow.GetComponent<SpriteRenderer>();
            ss.sprite = _pixel; ss.sortingOrder = 10; ss.color = new Color(0f, 0f, 0f, .28f);

            var bar = new GameObject("HealthBar", typeof(SpriteRenderer));
            bar.transform.SetParent(transform, false);
            bar.transform.localPosition = new Vector3(0f, .55f, 0f);
            bar.transform.localScale = new Vector3(.76f, .08f, 1f);
            var bs = bar.GetComponent<SpriteRenderer>();
            bs.sprite = _pixel; bs.sortingOrder = 30; bs.color = new Color(.08f, .08f, .07f, .9f);

            var fill = new GameObject("Fill", typeof(SpriteRenderer));
            fill.transform.SetParent(bar.transform, false);
            fill.transform.localPosition = new Vector3(-.48f, 0f, -.01f);
            fill.transform.localScale = new Vector3(.92f, .62f, 1f);
            var fs = fill.GetComponent<SpriteRenderer>();
            fs.sprite = _pixel; fs.sortingOrder = 31; fs.color = new Color(.45f, .86f, .38f, 1f);
            _barFill = fill.transform;
        }

        private void LateUpdate()
        {
            if (_unit == null || _barFill == null) return;
            float hp = Mathf.Clamp01(_unit.HealthPercent);
            _barFill.localScale = new Vector3(.92f * hp, .62f, 1f);
            if (_unit.IsDead) gameObject.SetActive(false);
        }
    }
}
