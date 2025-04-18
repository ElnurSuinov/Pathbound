using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class CardView : MonoBehaviour
{
    private Collider2D col;
    private Vector3 startDragPosition;
    private bool isDragging = false;

    [SerializeField] private TMP_Text title;

    [SerializeField] private TMP_Text description;

    [SerializeField] private TMP_Text energy;

    [SerializeField] private SpriteRenderer imageSR;

    [SerializeField] private GameObject wrapper;

    public Card Card { get; private set; }

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }
    public void Setup(Card card)
    {
        Card = card;
        title.text = card.Title;
        description.text = card.Description;
        energy.text = card.Energy.ToString();
        imageSR.sprite = card.Image;
    }

    //public void OnMouseDown()
    //{
        
    //    isDragging = true;
    //    wrapper.SetActive(false);
    //    startDragPosition = transform.position;
    //    transform.position = GetMousePositionInWorldSpace();
    //    transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
    //    transform.rotation = Quaternion.Euler(0, 0, 0);
    //}
    //public void OnMouseDrag()
    //{

    //    transform.position = GetMousePositionInWorldSpace();
    //}

    //public void OnMouseUp()
    //{
    //    isDragging = false;
    //    col.enabled = false;
    //    Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
    //    col.enabled = true;
    //    if (hitCollider != null && hitCollider.TryGetComponent(out CardDropArea cardDropArea))
    //    {
    //        cardDropArea.OnCardDrop(this);
    //    }
    //    else
    //    {
    //        transform.position = startDragPosition;
    //    }
    //}

    //public Vector3 GetMousePositionInWorldSpace()
    //{
    //    Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    p.z = 0f;
    //    return p;
    //}
    public void OnMouseEnter()
    {
        if(isDragging == false)
        {
        }
            wrapper.SetActive(false);
            Vector3 pos = new(transform.position.x, -1, 0);
            CardViewHoverSystem.Instance.Show(Card, pos);
    }
    public void OnMouseExit()
    {
        CardViewHoverSystem.Instance.Hide();
        wrapper.SetActive(true);
    }

}
