using UnityEngine;
using System;

#if UNITY_ANDROID
using Unity.Notifications.Android;
using UnityEngine.Android;
#endif

public class NotificationSimple : MonoBehaviour
{
    private const string defaultChannel = "default_channel";
    [SerializeField] private ScoreData scoreData;

    private void Start()
    {
#if UNITY_ANDROID
        RequestAuthorization();
        RegisterNotificationChannel();
#endif
    }

#if UNITY_ANDROID
    public void RequestAuthorization()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }
    }

    public void RegisterNotificationChannel()
    {
        AndroidNotificationChannel channel = new AndroidNotificationChannel
        {
            Id = defaultChannel,
            Name = "Game Notifications",
            Importance = Importance.High,
            Description = "Notifications for game events"
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public void SendScoreNotification(int currentScore)
    {
        if (currentScore > scoreData.MaxScore)
        {
            SendNewRecordNotification(currentScore);
        }
        else
        {
            SendRegularScoreNotification(currentScore);
        }
    }

    private void SendRegularScoreNotification(int score)
    {
        AndroidNotification notification = new AndroidNotification
        {
            Title = "Ronda Terminada",
            Text = $"Puntaje conseguido: {score}",
            FireTime = DateTime.Now.AddSeconds(5),
            SmallIcon = "score",
            LargeIcon = default,
        };

        AndroidNotificationCenter.SendNotification(notification, defaultChannel);
    }

    private void SendNewRecordNotification(int score)
    {
        AndroidNotification notification = new AndroidNotification
        {
            Title = "¡Nuevo Puntaje Máximo!",
            Text = $"¡Felicidades! Nuevo récord: {score}",
            FireTime = DateTime.Now.AddSeconds(5),
            SmallIcon = "maxscore",
            LargeIcon = default,

            Style = NotificationStyle.BigTextStyle
        };

        AndroidNotificationCenter.SendNotification(notification, defaultChannel);
    }
#endif
}