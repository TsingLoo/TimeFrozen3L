using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class AwakeScreenEffect : MonoBehaviour
{
    public FlowManager flowManger;

    [Range(0f, 1f)]
    [Tooltip("���ѽ���")]
    public float progress;
    public Shader shader;

    [Range(0, 4)]
    [Tooltip("ģ����������")]
    public int blurIterations = 3;
    [Range(.1f, 3f)]
    [Tooltip("ÿ��ģ������ʱ��ģ����С��ɢ")]
    public float blurSpread = .6f;

    [SerializeField]
    Material material;
    Material Material
    {
        get
        {
            if (material == null)
            {
                material = new Material(shader);
                material.hideFlags = HideFlags.DontSave;
            }
            return material;
        }
    }

    void Update()
    {
        if (flowManger.GameStage == -1)
        {
            if (progress == 1)
            {
                gameObject.GetComponent<Animator>().SetBool("waked", true);
                flowManger.GameStage = 0;

            }
        }
    }



    void OnDisable()
    {
        if (material)
        {
            DestroyImmediate(material);
        }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Material.SetFloat("_Progress", progress);
        if (progress < 1)
        {
            // ���ڽ�������Ӱ��ģ���������������ԣ�����û��ʹ��
            int rtW = src.width;
            int rtH = src.height;
            var buffer0 = RenderTexture.GetTemporary(rtW, rtH, 0);
            buffer0.filterMode = FilterMode.Bilinear;
            Graphics.Blit(src, buffer0, Material, 0);   // ��ƤPass
            // ģ��
            float blurSize;
            for (int i = 0; i < blurIterations; i++)
            {
                // ��progress(0~1)ӳ�䵽blurSize(blurSize~0)
                blurSize = 1f + i * blurSpread;
                blurSize = blurSize - blurSize * progress;
                Material.SetFloat("_BlurSize", blurSize);
                // ��ֱ�����Pass
                var buffer1 = RenderTexture.GetTemporary(rtW, rtH, 0);
                Graphics.Blit(buffer0, buffer1, Material, 1);
                RenderTexture.ReleaseTemporary(buffer0);
                // ��ֱ�����Pass
                buffer0 = buffer1;
                buffer1 = RenderTexture.GetTemporary(rtW, rtH, 0);
                Graphics.Blit(buffer0, buffer1, Material, 2);

                RenderTexture.ReleaseTemporary(buffer0);
                buffer0 = buffer1;
            }
            Graphics.Blit(buffer0, dest);
            RenderTexture.ReleaseTemporary(buffer0);
        }
        else
        {
            // ��ȫ���������账��ֱ��blit
            Graphics.Blit(src, dest);
            gameObject.GetComponent<Animator>().SetBool("waked", true);

            int rtW = src.width;
            int rtH = src.height;
            var buffer0 = RenderTexture.GetTemporary(rtW, rtH, 0);
            buffer0.filterMode = FilterMode.Bilinear;
            Graphics.Blit(src, buffer0, Material, 0);   // ��ƤPass
            // ģ��
            float blurSize;
            for (int i = 0; i < blurIterations; i++)
            {
                // ��progress(0~1)ӳ�䵽blurSize(blurSize~0)
                blurSize = 1f + i * blurSpread;
                blurSize = blurSize - blurSize * progress;
                Material.SetFloat("_BlurSize", blurSize);
                // ��ֱ�����Pass
                var buffer1 = RenderTexture.GetTemporary(rtW, rtH, 0);
                Graphics.Blit(buffer0, buffer1, Material, 1);
                RenderTexture.ReleaseTemporary(buffer0);
                // ��ֱ�����Pass
                buffer0 = buffer1;
                buffer1 = RenderTexture.GetTemporary(rtW, rtH, 0);
                Graphics.Blit(buffer0, buffer1, Material, 2);

                RenderTexture.ReleaseTemporary(buffer0);
                buffer0 = buffer1;
            }
            Graphics.Blit(buffer0, dest);
            RenderTexture.ReleaseTemporary(buffer0);

        }


    }
}
