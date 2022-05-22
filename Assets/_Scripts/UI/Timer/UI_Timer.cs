using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI.Timer
{
    public class UI_Timer : StaticInstance<UI_Timer>
    {
        [SerializeField] private TextMeshProUGUI textTimer;

        private float timeRemaining = Parameters.timeLimit;
        public delegate void OnTimer();
        public OnTimer onTimer;

        // private bool startTimer = GlobalParameters.startTimer;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            SubscribeGameManager();
        }

        private void Update()
        {
            SetTimer();
        }

        private void SetTimer()
        {
            if (!Parameters.startTimer) return;

            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                textTimer.text = $"{Mathf.RoundToInt(timeRemaining)} s";
            }
            else if (timeRemaining <= 0)
            {
                onTimer?.Invoke();
                ResetTimer();
            }
        }

        private void SubscribeGameManager()
        {
            GameManager.Instance.onReset += () =>
            {
                ResetTimer();
            };
        }

        private void ResetTimer()
        {
            Parameters.startTimer = false;
            timeRemaining = Parameters.timeLimit;
            textTimer.text = $"{Mathf.RoundToInt(timeRemaining)} s";
        }
    }
}
