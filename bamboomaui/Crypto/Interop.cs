
using System.Runtime.InteropServices;
using System.Security;

namespace Bamboomaui.Crypto
{
    [SuppressUnmanagedCodeSecurity]
    internal static class Secp256K1Native
    {
        private const string NativeLibrary = "libsecp256k1";

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr secp256k1_context_create(uint flags);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void secp256k1_context_destroy(IntPtr ctx);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int secp256k1_ec_seckey_verify(IntPtr ctx, byte[] seed32);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int secp256k1_ecdsa_verify(IntPtr ctx, byte[] sig, byte[] msg32, byte[] pubkey);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int secp256k1_ecdsa_sign(IntPtr ctx, byte[] sig, byte[] msg32, byte[] seckey, IntPtr noncefp, IntPtr ndata);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int secp256k1_ec_pubkey_serialize(IntPtr ctx, byte[] output, ref uint outputlen, byte[] pubkey, uint flags);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int secp256k1_ec_pubkey_create(IntPtr ctx, byte[] pubKeyOut, byte[] privKeyIn);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr secp256k1_scratch_space_create(IntPtr ctx, uint max_size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int secp256k1_scratch_space_destroy(IntPtr scratch);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int secp256k1_ec_pubkey_parse(IntPtr ctx, byte[] pubkey, byte[] input, int inputlen);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int secp256k1_context_randomize(IntPtr ctx, byte[] seed32);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int secp256k1_ecdh(IntPtr ctx, byte[] result, byte[] pubkey, byte[] privkey);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int secp256k1_ec_pubkey_tweak_add(IntPtr ctx, byte[] pubkey, byte[] tweak);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int secp256k1_ec_privkey_tweak_add(IntPtr ctx, byte[] seckey, byte[] tweak);

    }

    [SuppressUnmanagedCodeSecurity]
    internal static class PedersenNative
    {
        private const string NativeLibrary = "libsecp256k1";

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr secp256k1_context_create(uint flags);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void secp256k1_context_destroy(IntPtr ctx);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int secp256k1_pedersen_blind_sum(IntPtr ctx, byte[] blind_out, IntPtr[] blinds, uint n, uint npositive);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int secp256k1_pedersen_blind_commit(IntPtr ctx, byte[] commit, byte[] blind, byte[] value, byte[] value_gen, byte[] blind_gen);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int secp256k1_pedersen_commit(IntPtr ctx, byte[] commit, byte[] blind, ulong value, byte[] value_gen, byte[] blind_gen);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int secp256k1_pedersen_commitment_serialize(IntPtr ctx, byte[] output, byte[] commit);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int secp256k1_pedersen_commitment_parse(IntPtr ctx, byte[] commit, byte[] input);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int secp256k1_pedersen_commit_sum(IntPtr ctx, byte[] commit_out, IntPtr[] commits, uint pcnt, IntPtr[] ncommits, uint ncnt);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int secp256k1_pedersen_verify_tally(IntPtr ctx, IntPtr[] pos, uint n_pos, IntPtr[] neg, uint n_neg);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int secp256k1_blind_switch(IntPtr ctx, byte[] blind_switch, byte[] blind, ulong value, byte[] value_gen, byte[] blind_gen, byte[] switch_pubkey);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int secp256k1_pedersen_commitment_to_pubkey(IntPtr ctx, byte[] pubkey, byte[] commit);
    }

    [SuppressUnmanagedCodeSecurity]
    internal static class BulletProofNative
    {
        private const string NativeLibrary = "libsecp256k1";

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr secp256k1_bulletproof_generators_create(IntPtr ctx, byte[] blinding_gen, int n);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int secp256k1_bulletproof_rangeproof_prove(IntPtr ctx, IntPtr scratch, IntPtr gens, byte[] proof, ref int plen, byte[] tau_x, byte[] t_one, byte[] t_two, IntPtr[] value, IntPtr[] min_value,
            IntPtr[] blind, byte[] commits, int n_commits, byte[] value_gen, int nbits, byte[] nonce, byte[] private_nonce, byte[] extra_commit, int extra_commit_len, byte[] message);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int secp256k1_bulletproof_rangeproof_verify(IntPtr ctx, IntPtr scratch, IntPtr gens, byte[] proof, int plen, IntPtr[] min_value, byte[] commit, int n_commits, int nbits, byte[] value_gen,
            byte[] extra_commit, int extra_commit_len);
    }
    
    [SuppressUnmanagedCodeSecurity]
    internal static class MLSAGNative
    {
        private const string nativeLibrary = "libsecp256k1";
        
        [DllImport(nativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe int secp256k1_prepare_mlsag(
            [In, Out] void* pubkeys,
            [Out] void* blind_out,
            int n_outs,
            int n_blinds,
            int n_cols,
            int n_rows,
            [In] IntPtr[] inputs,
            [In] IntPtr[] outputs,
            [In] IntPtr[] blinds);
        
        [DllImport(nativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int secp256k1_get_keyimage(
            IntPtr ctx,
            byte[] output,
            byte[] pubkey,
            byte[] blind);
        
        [DllImport(nativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe int secp256k1_generate_mlsag(
            IntPtr ctx,
            [Out] void* imagekeys,
            [Out] void* pc,
            [Out] void* ps,
            [In] void* nonce32,
            [In] void* msg32,
            int n_cols,
            int n_rows,
            int index,
            [In] IntPtr[] blinds,
            [In] void* pubkeys);
        
        [DllImport(nativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe int secp256k1_verify_mlsag(
            IntPtr ctx,
            [In] void* msg32,
            int n_cols,
            int n_rows,
            [In] void* pubkeys,
            [In] void* imagekeys,
            [In] void* pc,
            [In] void* ps);
    }

    [Flags]
    public enum Flags : uint
    {
        /** All flags' lower 8 bits indicate what they're for. Do not use directly. */
        SECP256K1_FLAGS_TYPE_MASK = ((1 << 8) - 1),
        SECP256K1_FLAGS_TYPE_CONTEXT = (1 << 0),
        SECP256K1_FLAGS_TYPE_COMPRESSION = (1 << 1),

        /** The higher bits contain the actual data. Do not use directly. */
        SECP256K1_FLAGS_BIT_CONTEXT_VERIFY = (1 << 8),
        SECP256K1_FLAGS_BIT_CONTEXT_SIGN = (1 << 9),
        SECP256K1_FLAGS_BIT_COMPRESSION = (1 << 8),

        /** Flags to pass to secp256k1_context_create. */
        SECP256K1_CONTEXT_VERIFY = (SECP256K1_FLAGS_TYPE_CONTEXT | SECP256K1_FLAGS_BIT_CONTEXT_VERIFY),
        SECP256K1_CONTEXT_SIGN = (SECP256K1_FLAGS_TYPE_CONTEXT | SECP256K1_FLAGS_BIT_CONTEXT_SIGN),
        SECP256K1_CONTEXT_NONE = (SECP256K1_FLAGS_TYPE_CONTEXT),

        /** Flag to pass to secp256k1_ec_pubkey_serialize and secp256k1_ec_privkey_export. */
        SECP256K1_EC_COMPRESSED = (SECP256K1_FLAGS_TYPE_COMPRESSION | SECP256K1_FLAGS_BIT_COMPRESSION),
        SECP256K1_EC_UNCOMPRESSED = (SECP256K1_FLAGS_TYPE_COMPRESSION)
    }

}
